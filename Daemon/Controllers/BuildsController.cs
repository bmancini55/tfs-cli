using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Tfs_Cli.Daemon.Controllers
{
    public class BuildsController : BaseController
    {
        public List<object> GetBuilds(string name)
        {
            this.Options.BuildDefinition = name;

            var queryFactory = new QueryFactory();
            
            var collectionQuery = queryFactory.CreateCollectionQuery(this.Options);
            var collection = collectionQuery.Execute();

            var buildDefinitionsQuery = queryFactory.CreateBuildDefinitionsQuery(collection, this.Options);
            var buildDefinitions = buildDefinitionsQuery.Execute();

            var results = new List<object>();
            foreach (var buildDefinition in buildDefinitions)
            {
                var buildsQuery = queryFactory.CreateBuildsQuery(buildDefinition, this.Options);
                var builds = buildsQuery.Execute();
                var lastSuccessfulBuild = builds
                    .Where(p => p.Status == BuildStatus.Succeeded)
                    .OrderBy(p => p.FinishTime)
                    .FirstOrDefault();

                if (lastSuccessfulBuild != null)
                {
                    results.Add(new
                    {
                        Definition = lastSuccessfulBuild.BuildDefinition.Name,
                        BuildNumber = lastSuccessfulBuild.BuildNumber,
                        DropLocation = lastSuccessfulBuild.DropLocation,
                        Status = lastSuccessfulBuild.Status,
                        StartTime = lastSuccessfulBuild.StartTime,
                        FinishTime = lastSuccessfulBuild.FinishTime,
                    });
                }
            }
            return results;
        }
    }
}
