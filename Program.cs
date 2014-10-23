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

namespace tfs_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = GetCollection("FTICollection");
            var builds = GetBuilds(collection, "Ringtail", "LHOTSEFIX Ringtail8 Daily");

            foreach (var build in builds)
            {
                Console.WriteLine(build);
            }
        }

        static TfsTeamProjectCollection GetCollection(string collectionName)
        {
            Uri tfsUri = new Uri("http://tfs2012.dev.tech.local:8080/tfs");
            TfsConfigurationServer configurationServer = TfsConfigurationServerFactory.GetConfigurationServer(tfsUri);

            ReadOnlyCollection<CatalogNode> collectionNodes = configurationServer.CatalogNode.QueryChildren(
                new[] { CatalogResourceTypes.ProjectCollection },
                false, CatalogQueryOptions.None);

            var collectionNode = collectionNodes.FirstOrDefault(p => p.Resource.DisplayName == collectionName);
            if (collectionNode != null)
            {
                Guid collectionId = new Guid(collectionNode.Resource.Properties["InstanceId"]);
                TfsTeamProjectCollection teamProjectCollection = configurationServer.GetTeamProjectCollection(collectionId);
                return teamProjectCollection;
            }
            return null;
        }

        static List<IBuildDetail> GetBuilds(TfsTeamProjectCollection projectCollection, string project, string definition)
        {
            var buildServer = (IBuildServer)projectCollection.GetService(typeof(IBuildServer));
            var buildDefinition = buildServer.GetBuildDefinition(project, definition);
            var builds = buildDefinition.QueryBuilds();
            return builds.ToList();
        }

    }
}
