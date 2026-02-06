namespace Pozitron.SharedKernel;

public interface IAppLogger<T>
{
    void LogTrace(string? message);
    void LogTrace<T0>(string? message, T0 arg);
    void LogTrace<T0, T1>(string? message, T0 arg0, T1 arg1);
    void LogTrace<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogTrace(string? message, params object?[] args);

    void LogTrace(Exception? exception, string? message);
    void LogTrace<T0>(Exception? exception, string? message, T0 arg);
    void LogTrace<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    void LogTrace<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogTrace(Exception? exception, string? message, params object?[] args);

    void LogDebug(string? message);
    void LogDebug<T0>(string? message, T0 arg);
    void LogDebug<T0, T1>(string? message, T0 arg0, T1 arg1);
    void LogDebug<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogDebug(string? message, params object?[] args);

    void LogDebug(Exception? exception, string? message);
    void LogDebug<T0>(Exception? exception, string? message, T0 arg);
    void LogDebug<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    void LogDebug<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogDebug(Exception? exception, string? message, params object?[] args);

    void LogInformation(string? message);
    void LogInformation<T0>(string? message, T0 arg);
    void LogInformation<T0, T1>(string? message, T0 arg0, T1 arg1);
    void LogInformation<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogInformation(string? message, params object?[] args);

    void LogInformation(Exception? exception, string? message);
    void LogInformation<T0>(Exception? exception, string? message, T0 arg);
    void LogInformation<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    void LogInformation<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogInformation(Exception? exception, string? message, params object?[] args);

    void LogWarning(string? message);
    void LogWarning<T0>(string? message, T0 arg);
    void LogWarning<T0, T1>(string? message, T0 arg0, T1 arg1);
    void LogWarning<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogWarning(string? message, params object?[] args);

    void LogWarning(Exception? exception, string? message);
    void LogWarning<T0>(Exception? exception, string? message, T0 arg);
    void LogWarning<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    void LogWarning<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogWarning(Exception? exception, string? message, params object?[] args);

    void LogError(string? message);
    void LogError<T0>(string? message, T0 arg);
    void LogError<T0, T1>(string? message, T0 arg0, T1 arg1);
    void LogError<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogError(string? message, params object?[] args);

    void LogError(Exception? exception, string? message);
    void LogError<T0>(Exception? exception, string? message, T0 arg);
    void LogError<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    void LogError<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogError(Exception? exception, string? message, params object?[] args);

    void LogCritical(string? message);
    void LogCritical<T0>(string? message, T0 arg);
    void LogCritical<T0, T1>(string? message, T0 arg0, T1 arg1);
    void LogCritical<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogCritical(string? message, params object?[] args);

    void LogCritical(Exception? exception, string? message);
    void LogCritical<T0>(Exception? exception, string? message, T0 arg);
    void LogCritical<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    void LogCritical<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    void LogCritical(Exception? exception, string? message, params object?[] args);
}
