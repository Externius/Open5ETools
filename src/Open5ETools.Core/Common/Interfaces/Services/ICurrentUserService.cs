namespace Open5ETools.Core.Common.Interfaces.Services;
public interface ICurrentUserService
{
    int GetUserIdAsInt();
    string UserId { get; }
    string UserName { get; }
}