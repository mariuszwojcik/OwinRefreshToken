using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Security;
using System.Web.SessionState;

namespace AngularJsWebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            MinifyJavaScriptAndCSS();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }


        public static void MinifyJavaScriptAndCSS()
        {
            var angularScripts = new ScriptBundle("~/bundles/angular")
                .IncludeDirectory(@"~/Controllers", "*.js", true)
                .IncludeDirectory(@"~/Model", "*.js", true)
                .Include("~/SampleApp.js");
            BundleTable.Bundles.Add(angularScripts);

            //Bundle Css
            //var css1 = new StyleBundle("~/bundles/customCSSBundle");
            //css1.Include("~/Styles/style1.css");
            //css1.Include("~/Styles/style2.css");
            //BundleTable.Bundles.Add(css1);
        }
    }
}