using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks.System;

public class MaximumValueHealthCheck<T> : IHealthCheck
    where T : IComparable<T>
{
    private readonly T _maximumValue;
    private readonly Func<T> _currentValueFunc;

    public MaximumValueHealthCheck(T maximumValue, Func<T> currentValueFunc)
    {
        _maximumValue = maximumValue;
        _currentValueFunc = Guard.ThrowIfNull(currentValueFunc);
    }

    /// <inheritdoc />
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var currentValue = _currentValueFunc();

        if (currentValue.CompareTo(_maximumValue) <= 0)
        {
            return HealthCheckResultTask.Healthy;
        }

        return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, description: $"Maximum={_maximumValue}, Current={currentValue}"));
    }
}
