namespace JukeBox.Services;

public interface IRetryPolicyService
{
   public Polly.Retry.AsyncRetryPolicy RetryGet();
}