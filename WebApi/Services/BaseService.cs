using Polly;

namespace WebApi.Services
{
    public abstract class BaseService<T>
    {
        private const int DefaultRetryAttempts = 3;
        private const int DefaultRetryIntervalMilliseconds = 100;
        private const int DefaultMaxRetryIntervalSeconds = 100;

        protected readonly ILogger<T> Logger;
        protected readonly IAsyncPolicy RetryPolicy;

        protected BaseService(ILogger<T> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            RetryPolicy = SetupRetryPolicy(DefaultRetryAttempts, DefaultRetryIntervalMilliseconds, DefaultMaxRetryIntervalSeconds);
        }

        private IAsyncPolicy SetupRetryPolicy(int retryCount, int retryIntervalMilliseconds, int maxRetrySeconds)
        {
            var result = Policy.Handle<Exception>()
                .WaitAndRetryAsync(
                    retryCount: retryCount,
                    sleepDurationProvider:
                    retryAttempt =>
                    {
                        //Exponential back-off retry logic
                        var exponent = retryAttempt - 1;
                        var nextRetryDelay = TimeSpan.FromMilliseconds(retryIntervalMilliseconds * Math.Pow(2, exponent));
                        var retryDelayInSeconds = Math.Min(nextRetryDelay.TotalSeconds, maxRetrySeconds);

                        return TimeSpan.FromSeconds(retryDelayInSeconds);
                    },
                    onRetry: (ex, sleepDuration, attemptNumber) =>
                    {
                        Logger.LogWarning(
                            ex,
                            "RetryPolicy attempt. SleepDuration: {sleepDuration}, Attempts: {attemptNumber}",
                            sleepDuration,
                            attemptNumber
                        );
                    });

            return result;
        }
    }
}
