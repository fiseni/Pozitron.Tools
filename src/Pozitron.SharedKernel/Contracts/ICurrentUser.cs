namespace Pozitron.SharedKernel;

public interface ICurrentUser
{
    string? UserId { get; }
    string? Username { get; }
    string? Email { get; }
    string? FullName { get; }
    string? Roles { get; }
}
