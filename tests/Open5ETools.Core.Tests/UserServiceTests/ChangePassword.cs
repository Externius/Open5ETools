using Microsoft.Extensions.Options;
using Open5ETools.Core.Common.Configurations;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Infrastructure.Data;
using Open5ETools.Resources;
using Shouldly;

namespace Open5ETools.Core.Tests.UserServiceTests;

public class ChangePassword(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly IUserService _userService = fixture.UserService;
    private readonly IOptions<AppConfigOptions> _config = fixture.Config;
    private const string Password = "SOMENEWPASSWORD123";

    [Fact]
    public async Task ChangePasswordAsync_WithValidInput_ChangesPassword()
    {
        var model = new ChangePasswordModel
        (
            AppDbContextInitializer.TestUserId,
            _config.Value.DefaultUserPassword,
            Password
        );
        var oldUserModel = await _userService.GetAsync(model.Id, TestContext.Current.CancellationToken);
        var act = async () => { await _userService.ChangePasswordAsync(model, TestContext.Current.CancellationToken); };

        await act.ShouldNotThrowAsync();
        var newUserModel = await _userService.GetAsync(model.Id, TestContext.Current.CancellationToken);
        newUserModel.Password.ShouldNotBe(oldUserModel.Password);
    }

    public static TheoryData<ChangePasswordModel, string> GetModelsWithErrors()
    {
        return
        [
            (new ChangePasswordModel
            (
                AppDbContextInitializer.TestUserId,
                Password,
                "length"
            ), Error.PasswordLength),
            (new ChangePasswordModel
            (
                AppDbContextInitializer.TestNotExistingUserId,
                string.Empty,
                Password
            ), Error.NotFound),
            (new ChangePasswordModel
            (
                AppDbContextInitializer.TestUserId,
                "wrong",
                Password
            ), Error.PasswordMissMatch)
        ];
    }

    [Theory]
    [MemberData(nameof(GetModelsWithErrors), MemberType = typeof(ChangePassword))]
    public async Task ChangePasswordAsync_WithInValidModel_ThrowsServiceException(ChangePasswordModel model,
        string expectedError)
    {
        var act = async () =>
        {
            await _userService.ChangePasswordAsync(model, cancellationToken: TestContext.Current.CancellationToken);
        };

        var result = await act.ShouldThrowAsync<ServiceException>();
        result.Message.ShouldBe(expectedError);
    }
}