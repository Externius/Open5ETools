using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Infrastructure.Data;
using Shouldly;

namespace Open5ETools.Core.Tests.UserServiceTests;

public class Delete(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly IUserService _userService = fixture.UserService;

    [Fact]
    public async Task DeleteAsync_WithValidId_ReturnsTrue()
    {
        var result = await _userService.DeleteAsync(AppDbContextInitializer.TestAdminUserId,
            cancellationToken: TestContext.Current.CancellationToken);
        result.ShouldBe(true);
    }

    [Fact]
    public async Task DeleteAsync_WithInValidId_ReturnsFalse()
    {
        var result = await _userService.DeleteAsync(AppDbContextInitializer.TestNotExistingUserId,
            cancellationToken: TestContext.Current.CancellationToken);
        result.ShouldBe(false);
    }
}