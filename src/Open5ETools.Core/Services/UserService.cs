using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Open5ETools.Core.Common.Enums;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Core.Domain;
using Open5ETools.Core.Helpers;

namespace Open5ETools.Core.Services;

public class UserService(IMapper mapper, IAppDbContext context, ILogger<UserService> logger) : IUserService
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<int> CreateAsync(UserModel model, CancellationToken cancellationToken)
    {
        ValidateModel(model);
        await CheckUserExistAsync(model, cancellationToken);
        try
        {
            model.Password = PasswordHelper.EncryptPassword(model.Password);
            var user = _mapper.Map<User>(model);
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create User failed.");
            throw;
        }
    }

    private static void ValidateModel(UserModel model)
    {
        var errors = new List<ServiceException>();
        ArgumentNullException.ThrowIfNull(model);
        if (model.Password.Length < 8)
            errors.Add(new ServiceException(Resources.Error.PasswordLength));
        if (string.IsNullOrEmpty(model.Username))
            errors.Add(new ServiceException(string.Format(Resources.Error.RequiredValidation, model.Username)));
        if (string.IsNullOrEmpty(model.FirstName))
            errors.Add(new ServiceException(string.Format(Resources.Error.RequiredValidation, model.FirstName)));
        if (string.IsNullOrEmpty(model.LastName))
            errors.Add(new ServiceException(string.Format(Resources.Error.RequiredValidation, model.LastName)));
        if (string.IsNullOrEmpty(model.Email))
            errors.Add(new ServiceException(string.Format(Resources.Error.RequiredValidation, model.Email)));
        if (string.IsNullOrEmpty(model.Role))
            errors.Add(new ServiceException(string.Format(Resources.Error.RequiredValidation, model.Role)));
        if (errors.Count != 0)
            throw new ServiceAggregateException(errors);
    }

    private async Task CheckUserExistAsync(UserModel model, CancellationToken cancellationToken)
    {
        var user = await _context.Users
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.Username == model.Username, cancellationToken);
        if (user is not null)
            throw new ServiceException(string.Format(Resources.Error.UserExist, model.Username));
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (user is not null)
            {
                user.IsDeleted = true;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }

            _logger.LogError("Entity not found (User# {id})", id);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete User failed.");
            throw;
        }
    }

    public async Task<UserModel> GetAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            return _mapper.Map<UserModel>(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get User failed.");
            throw;
        }
    }

    public async Task<IEnumerable<UserModel>> ListAsync(bool? deleted = false, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _context.Users.AsNoTracking();

            if (deleted.HasValue)
                query = query.Where(x => x.IsDeleted == deleted.Value);

            return (await query.ToListAsync(cancellationToken))
                                .Select(_mapper.Map<UserModel>)
                                .OrderBy(um => um.Username)
                                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "List Users failed.");
            throw;
        }
    }

    public async Task<bool> RestoreAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user is not null)
            {
                user.IsDeleted = false;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Restore User #{userid} failed.", id);
            throw;
        }
    }

    public async Task UpdateAsync(UserModel model, CancellationToken cancellationToken)
    {
        ValidateModelForEdit(model);
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id, cancellationToken);
            if (user is not null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Role = (Role)Enum.Parse(typeof(Role), model.Role);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update User failed.");
            throw;
        }
    }

    private static void ValidateModelForEdit(UserModel model)
    {
        var errors = new List<ServiceException>();
        ArgumentNullException.ThrowIfNull(model);
        if (string.IsNullOrEmpty(model.FirstName))
            errors.Add(new ServiceException(string.Format(Resources.Error.RequiredValidation, model.FirstName)));
        if (string.IsNullOrEmpty(model.LastName))
            errors.Add(new ServiceException(string.Format(Resources.Error.RequiredValidation, model.LastName)));
        if (string.IsNullOrEmpty(model.Email))
            errors.Add(new ServiceException(string.Format(Resources.Error.RequiredValidation, model.Email)));
        if (string.IsNullOrEmpty(model.Role))
            errors.Add(new ServiceException(string.Format(Resources.Error.RequiredValidation, model.Role)));
        if (errors.Count != 0)
            throw new ServiceAggregateException(errors);
    }

    public async Task ChangePasswordAsync(ChangePasswordModel model, CancellationToken cancellationToken)
    {
        try
        {
            if (model.NewPassword.Length < 8)
                throw new ServiceException(Resources.Error.PasswordLength);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id, cancellationToken) ?? throw new ServiceException(Resources.Error.NotFound);

            if (!PasswordHelper.CheckPassword(user.Password, model.CurrentPassword))
                throw new ServiceException(Resources.Error.PasswordMissMatch);

            user.Password = PasswordHelper.EncryptPassword(model.NewPassword);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Change user password failed.");
            throw;
        }
    }
}