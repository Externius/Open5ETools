using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Infrastructure.Data;
using Shouldly;

namespace Open5ETools.Core.Tests.UserServiceTests;

public class Get(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly IUserService _userService = fixture.UserService;

    [Theory]
    [InlineData(AppDbContextInitializer.TestAdminUserId)]
    [InlineData(AppDbContextInitializer.TestUserId)]
    [InlineData(AppDbContextInitializer.TestDeletedUserId)]
    public async Task GetAsync_WithValidId_ReturnsUser(int id)
    {
        var result = await _userService.GetAsync(id, TestContext.Current.CancellationToken);
        result.ShouldNotBeNull();
        result.Id.ShouldBe(id);
    }
}