using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;

namespace CMS.Server.Components.Shared.Components;

public abstract partial class BaseComponent<T> : ComponentBase where T : class
{
    [Inject] required public NavigationManager Navigation { get; set; }
    [Inject] required public ILogger<T> _logger { get; set; }

    private bool _isLoading = false;
    private string _errorMessage = string.Empty;
    private string _successMessage = string.Empty;

    protected bool IsLoading => _isLoading;
    protected string? ErrorMessage => _errorMessage;
    protected string? SuccessMessage => _successMessage;

    protected void SetLoading(bool isLoading)
    {
        _isLoading = isLoading;
    }

    protected void SetError(string message)
    {
        _errorMessage = message;
        _successMessage = string.Empty;
    }

    protected void SetSuccess(string message)
    {
        _successMessage = message;
        _errorMessage = string.Empty;
    }

    protected void NavigateTo(string url)
    {
        Navigation.NavigateTo(url);
    }

    protected async Task ExecuteWithLoadingAsync(Func<Task> operation)
    {
        SetLoading(true);

        try
        {
            await operation();
        }
        catch (Exception ex)
        {
            LogError(ex);
            SetError("An error occurred. Please try again later.");
        }
        finally
        {
            SetLoading(false);
        }
    }

    protected void LogError(Exception ex)
    {
        _logger?.LogError(ex.Message);
    }

    // Utility methods for structured logging with scopes
    protected IDisposable BeginScope<TState>(TState state)
    {
        return _logger.BeginScope(state)!;
    }

    protected void LogInformation<TState>(TState state, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.LogInformation(message, args);
        }
    }

    protected void LogWarning<TState>(TState state, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.LogWarning(message, args);
        }
    }

    protected void LogError<TState>(TState state, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.LogError(message, args);
        }
    }

    protected void LogCritical<TState>(TState state, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.LogCritical(message, args);
        }
    }

    protected void LogDebug<TState>(TState state, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.LogDebug(message, args);
        }
    }

    protected void LogTrace<TState>(TState state, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.LogTrace(message, args);
        }
    }

    protected void Log<TState>(LogLevel logLevel, TState state, Exception exception, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.Log(logLevel, exception, message, args);
        }
    }

    protected void Log<TState>(LogLevel logLevel, TState state, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.Log(logLevel, message, args);
        }
    }

    protected void Log<TState>(LogLevel logLevel, TState state, EventId eventId, Exception exception, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.Log(logLevel, eventId, exception, message, args);
        }
    }

    protected void Log<TState>(LogLevel logLevel, TState state, EventId eventId, string message, params object[] args)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.Log(logLevel, eventId, message, args);
        }
    }

    protected void Log<TState>(LogLevel logLevel, TState state, string message, Exception exception)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.Log(logLevel, exception, message);
        }
    }

    protected void Log<TState>(LogLevel logLevel, TState state, string message)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.Log(logLevel, message);
        }
    }

    protected void Log<TState>(LogLevel logLevel, TState state, EventId eventId, string message)
    {
        using (var scope = BeginScope(state))
        {
            _logger?.Log(logLevel, eventId, message);
        }
    }
}