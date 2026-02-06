namespace Pozitron.SharedKernel;

public interface ICurrentUserInitializer
{
    string? UserId { get; set; }
    string? Username { get; set; }
    string? Email { get; set; }
    string? FullName { get; set; }
    string? Roles { get; set; }
}
