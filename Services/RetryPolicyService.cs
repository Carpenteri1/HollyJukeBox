using Polly;

namespace JukeBox.Services;

public class RetryPolicyService : IRetryPolicyService
{
    public Polly.Retry.AsyncRetryPolicy RetryGet()
    {
        return Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}