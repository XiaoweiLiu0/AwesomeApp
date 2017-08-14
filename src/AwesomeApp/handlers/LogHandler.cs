using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeApp.handlers
{
    public class LogHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logger = (IMyLogger) request.GetDependencyScope().GetService(typeof(IMyLogger));
            var startAt = DateTime.UtcNow;
            logger.Log($"{request.RequestUri} start at {startAt}");

            return base.SendAsync(request, cancellationToken).ContinueWith(t =>
            {
                var endAt = DateTime.UtcNow;
                logger.Log($"end at {endAt}");

                var perf = Convert.ToInt64((endAt - startAt).TotalMilliseconds);
                logger.Log($"performance: {perf}");

                return t.Result;
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}