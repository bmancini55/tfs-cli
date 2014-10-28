using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Runner(options);
            }           
        }               

        static void Runner(Options options) {
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
