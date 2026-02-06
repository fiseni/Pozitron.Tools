namespace Pozitron.SharedKernel;

public class CurrentUserTest : ICurrentUser, ICurrentUserInitializer
{
    public string? UserId { get; set; } = "111";
    public string? Username { get; set; } = "TestUser";
    public string? Roles { get; set; } = "1";
    public string? Email { get; set; } = "test@local.com";
    public string? FullName { get; set; } = "TestFirstName TestLastName";
}
