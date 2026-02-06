namespace Pozitron.SharedKernel;

public interface IEntity<out TId>
{
    public TId Id { get; }
}
