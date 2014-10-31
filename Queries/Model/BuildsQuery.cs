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
    public class BuildsQuery : IQuery<IEnumerable<Build>>
    {
        public BuildsQuery(string tfsUri, string collectionName, string project, string buildDefinition)
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

        public IEnumerable<Build> Execute()
        {            
            var collectionQuery = new TfsCollectionQuery(this.TfsUri, this.CollectionName);
            var collection = collectionQuery.Execute();

            var buildDefinitionsQuery = new TfsBuildDefinitionsQuery(collection, this.Project, this.BuildDefinition);
            var buildDefinition = buildDefinitionsQuery.Execute().FirstOrDefault();

            var results = new List<Build>();            
            if(buildDefinition != null)
            {
                var buildsQuery = new TfsBuildsQuery(buildDefinition);
                var builds = buildsQuery.Execute();
                
                foreach(var build in builds)
                {
                    results.Add(new Build()
                    {
                        BuildNumber = build.BuildNumber,
                        Definition = build.BuildDefinition.Name,
                        DropLocation = build.DropLocation,
                        FinishTime = build.FinishTime,
                        StartTime = build.StartTime,
                        Status = build.Status
                    });
                }                
            }
            return results;
        }
    }
}
