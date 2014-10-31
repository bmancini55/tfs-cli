using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Web.Script.Serialization;
using TfsCli.Queries;
using TfsCli.Queries.Model;

namespace TfsCli
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (options.Daemon)
                    StartDaemon(options);                
                else                
                    ActionRunner(options);                
            }           
        }   
        
        static void StartDaemon(Options options)
        {
            var address = "http://localhost:3456";
            var config = new HttpSelfHostConfiguration(address);
            config.Properties["Options"] = options;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ProjectRoute",
                routeTemplate: "api/{collection}/{project}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            

            var server = new HttpSelfHostServer(config);

            server.OpenAsync().Wait();
            Console.WriteLine("Listening on " + address);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void ActionRunner(Options options) {

            switch (options.Action.ToLower())
            {
                case "build-defs":
                    {
                            
                    }
                    break;
                case "builds":
                    {
                        var query = new BuildsQuery(options.TfsUri, options.Collection, options.Project, options.BuildDefinition);
                        var builds = query.Execute();
                        Output(builds);
                    }
                    break;
                case "last-build":
                    {
                        var query = new LastBuildQuery(options.TfsUri, options.Collection, options.Project, options.BuildDefinition);
                        var build = query.Execute();
                        Output(build);
                    }
                    break;
            }
        }

        static void Output(object obj)
        {
            var output = new JavaScriptSerializer().Serialize(obj);
            Console.WriteLine(output);
        }
    }
}
