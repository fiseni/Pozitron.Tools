namespace Pozitron.SharedKernel;

public class CurrentUserDefault : ICurrentUser, ICurrentUserInitializer
{
    public string? UserId { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? Roles { get; set; }
}
