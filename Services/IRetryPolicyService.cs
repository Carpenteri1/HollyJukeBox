namespace HollyJukeBox.Services;

public interface IRetryPolicyService
{
   public Polly.Retry.AsyncRetryPolicy RetryGet();
}