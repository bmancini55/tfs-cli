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
using Tfs_Cli.Queries;

namespace Tfs_Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (options.Daemon)
                {
                    StartDaemon(options);
                }
                else
                {
                    ActionRunner(options);
                }
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
            

            var server = new HttpSelfHostServer(config);

            server.OpenAsync().Wait();
            Console.WriteLine("Listening on " + address);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void ActionRunner(Options options) {
            var queryFactory = new QueryFactory();
            switch (options.Action.ToLower())
            {
                case "build-defs":
                    {
                        var collectionQuery = queryFactory.CreateCollectionQuery(options);
                        var collection = collectionQuery.Execute();

                        var buildDefinitionsQuery = queryFactory.CreateBuildDefinitionsQuery(collection, options);
                        var buildDefinitions = buildDefinitionsQuery.Execute();

                        foreach (var buildDefinition in buildDefinitions)
                        {
                            Output(buildDefinition);
                        }
                    }
                    break;
                case "build-success":
                    {
                        var collectionQuery = queryFactory.CreateCollectionQuery(options);
                        var collection = collectionQuery.Execute();

                        var buildDefinitionsQuery = queryFactory.CreateBuildDefinitionsQuery(collection, options);
                        var buildDefinitions = buildDefinitionsQuery.Execute();

                        foreach (var buildDefinition in buildDefinitions)
                        {
                            var buildsQuery = queryFactory.CreateBuildsQuery(buildDefinition, options);
                            var builds = buildsQuery.Execute();
                            var lastSuccessfulBuild = builds
                                .Where(p => p.Status == BuildStatus.Succeeded)
                                .OrderBy(p => p.FinishTime)
                                .FirstOrDefault();

                            Output(lastSuccessfulBuild);
                        }
                    }
                    break;
            }
        }

        static void Output(IBuildDefinition buildDefinition)
        {
            if(buildDefinition != null)
            {
                var obj = new
                {
                    Name = buildDefinition.Name
                };

                Console.WriteLine(obj);
            }
        }

        static void Output(IBuildDetail build) 
        {
            if (build != null)
            {
                var obj = new
                {
                    Definition = build.BuildDefinition.Name,
                    BuildNumber = build.BuildNumber,
                    DropLocation = build.DropLocation,
                    Status = build.Status,
                    StartTime = build.StartTime,
                    FinishTime = build.FinishTime,
                };

                Console.WriteLine(obj);
            }
        }
    }
}
