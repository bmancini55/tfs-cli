using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tfs_Cli.Queries
{
    public class CollectionQuery : IQuery<TfsTeamProjectCollection>
    {
        public string TfsUri { get; set; }
        public string CollectionName { get; set; }

        public TfsTeamProjectCollection Execute()
        {
            Uri tfsUri = new Uri(TfsUri);
            TfsConfigurationServer configurationServer = TfsConfigurationServerFactory.GetConfigurationServer(tfsUri);

            ReadOnlyCollection<CatalogNode> collectionNodes = configurationServer.CatalogNode.QueryChildren(
                new[] { CatalogResourceTypes.ProjectCollection },
                false, CatalogQueryOptions.None);

            var collectionNode = collectionNodes.FirstOrDefault(p => p.Resource.DisplayName == this.CollectionName);
            if (collectionNode != null)
            {
                Guid collectionId = new Guid(collectionNode.Resource.Properties["InstanceId"]);
                TfsTeamProjectCollection teamProjectCollection = configurationServer.GetTeamProjectCollection(collectionId);
                return teamProjectCollection;
            }
            return null;
        }
    }
}
