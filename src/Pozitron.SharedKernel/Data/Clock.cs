namespace Pozitron.SharedKernel;

public class Clock
{
    private static readonly object _lock = new();

    private static IDateTime? _dateTimeProvider;
    public static IDateTime Provider => _dateTimeProvider ?? throw new NotImplementedException();
    public static DateTimeOffset Now => _dateTimeProvider?.Now ?? throw new NotImplementedException();

    public static IDateTime Initialize(IDateTime? dateTimeProvider = null)
    {
        if (_dateTimeProvider is null)
        {
            lock (_lock)
            {
                _dateTimeProvider ??= dateTimeProvider ?? DateTimeProvider.Implementation;
            }
        }

        return _dateTimeProvider;
    }

    private sealed class DateTimeProvider : IDateTime
    {
        public static readonly DateTimeProvider Implementation = new();
        private DateTimeProvider() { }

        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
