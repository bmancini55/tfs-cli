using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsCli.Models;
using TfsCli.Queries.Tfs;

namespace TfsCli.Queries.Model
{
    public class BuildDefinitionsQuery: IQuery<IEnumerable<BuildDefinition>>
    {
         public BuildDefinitionsQuery(string tfsUri, string collectionName, string project, string buildDefinition)
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

        public IEnumerable<BuildDefinition> Execute()
        {
            var collectionQuery = new TfsCollectionQuery(this.TfsUri, this.CollectionName);
            var collection = collectionQuery.Execute();

            var buildDefinitionsQuery = new TfsBuildDefinitionsQuery(collection, this.Project, this.BuildDefinition);
            var buildDefinitions = buildDefinitionsQuery.Execute();

            return buildDefinitions.Select(p => new BuildDefinition()
            {
                Name = p.Name
            });
        }
    }
}
