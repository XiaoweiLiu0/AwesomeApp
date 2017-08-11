using System;
using System.IO;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AwesomeApp.filters
{
    class LogFilter : ActionFilterAttribute
    {
        string startAt = "StartAt";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var time = DateTime.UtcNow;
            actionContext.Request.Properties[startAt] = time;
            var logger = (IMyLogger)actionContext.Request.GetDependencyScope().GetService(typeof(IMyLogger));
            logger.Log($"start at {time}");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var time = DateTime.UtcNow;
            var logger = (IMyLogger)actionExecutedContext.Request.GetDependencyScope().GetService(typeof(IMyLogger));
            logger.Log($"end at {time}");

            var perf = Convert.ToInt64((time - (DateTime) actionExecutedContext.Request.Properties[startAt]).TotalMilliseconds);
            logger.Log($"performance: {perf}");
        }
    }

    public class MyLogger : IMyLogger, IDisposable
    {
        readonly StreamWriter file = new StreamWriter(@"C:\\awesomeLog\log.txt");
        public void Log(string line)
        {
            file.WriteLine(line);
        }

        public void Dispose()
        {
            file?.Dispose();
        }
    }
}