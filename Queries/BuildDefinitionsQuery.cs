using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tfs_Cli.Queries
{
    public class BuildDefinitionsQuery : IQuery<List<IBuildDefinition>>
    {
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
