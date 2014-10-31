using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsCli.Models;
using TfsCli.Queries.Tfs;

namespace TfsCli.Queries.Model
{
    public class LastSuccessfulBuildQuery : IQuery<Build>
    {
        public LastSuccessfulBuildQuery(string tfsUri, string collectionName, string project, string buildDefinition)
        {
            this.TfsUri = tfsUri;
            this.CollectionName = collectionName;
            this.Project = project;
            this.BuildDefinition = buildDefinition;
        }

        public string TfsUri { get; set; }
        public string CollectionName { get; set; }
        public string Project { get; set; }
        public string BuildDefinition { get; set; }

        public Build Execute()
        {            
            var collectionQuery = new TfsCollectionQuery(this.TfsUri, this.CollectionName);
            var collection = collectionQuery.Execute();

            var buildDefinitionsQuery = new TfsBuildDefinitionsQuery(collection, this.Project, this.BuildDefinition);
            var buildDefinition = buildDefinitionsQuery.Execute().FirstOrDefault();

            
            if(buildDefinition != null)
            {
                var buildsQuery = new TfsBuildsQuery(buildDefinition);
                var builds = buildsQuery.Execute();
                var lastSuccessfulBuild = builds
                    .Where(p => p.Status == BuildStatus.Succeeded)
                    .OrderBy(p => p.FinishTime)
                    .FirstOrDefault();

                if(lastSuccessfulBuild != null) 
                {
                    return new Build()
                    {
                        BuildNumber = lastSuccessfulBuild.BuildNumber,
                        Definition = lastSuccessfulBuild.BuildDefinition.Name,
                        DropLocation = lastSuccessfulBuild.DropLocation,
                        FinishTime = lastSuccessfulBuild.FinishTime,
                        StartTime = lastSuccessfulBuild.StartTime,
                        Status = lastSuccessfulBuild.Status
                    };
                }                
            }
            return null;
        }
    }
}
