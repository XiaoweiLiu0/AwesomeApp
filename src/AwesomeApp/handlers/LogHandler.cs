using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeApp.handlers
{
    public class LogHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var startTime = DateTime.UtcNow;

            var logger = (IMyLogger)request.GetDependencyScope().GetService(typeof(IMyLogger));
            logger.Log($"{request.RequestUri} start at {startTime}");

            return base.SendAsync(request, cancellationToken)
                .ContinueWith(t =>
                {
                    var endTime = DateTime.UtcNow;
                    logger.Log($"{request.RequestUri} end at {endTime}");

                    var perf = Convert.ToInt64((endTime - startTime).TotalMilliseconds);
                    logger.Log($"{request.RequestUri} perf is {perf}");

                    return t.Result;
                }, cancellationToken);
        }
    }
}