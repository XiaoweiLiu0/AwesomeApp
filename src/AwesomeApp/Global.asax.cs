using System;
using System.Web.Http;

namespace AwesomeApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var configuration = GlobalConfiguration.Configuration;
            Bootstrapper.Init(configuration);
        }

    }
}