namespace Pozitron.SharedKernel;

public interface IDateTime
{
    DateTimeOffset Now { get; }
}
