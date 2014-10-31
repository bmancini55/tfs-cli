using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsCli.Queries.Tfs
{
    public class TfsBuildDefinitionsQuery : IQuery<List<IBuildDefinition>>
    {
        public TfsBuildDefinitionsQuery(TfsTeamProjectCollection projectCollection, string project, string buildDefinition)
        {
            this.ProjectCollection = projectCollection;
            this.Project = project;
            this.BuildDefinition = buildDefinition;
        }

        public TfsTeamProjectCollection ProjectCollection { get; set; }
        public string Project { get; set; }
        public string BuildDefinition { get; set; }

        public List<IBuildDefinition> Execute()
        {
            var buildServer = (IBuildServer)this.ProjectCollection.GetService(typeof(IBuildServer));

            var spec = buildServer.CreateBuildDefinitionSpec(Project, BuildDefinition);
            var buildDefinitionResults = buildServer.QueryBuildDefinitions(spec);

            return buildDefinitionResults.Definitions.ToList();
        }
    }
}
