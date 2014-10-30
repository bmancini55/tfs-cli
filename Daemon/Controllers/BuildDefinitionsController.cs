using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Tfs_Cli.Daemon.Controllers
{
    public class BuildDefinitionsController : BaseController
    {
        public IEnumerable<object> GetBuildDefinitionsByName(string name)
        {
            var queryFactory = new QueryFactory();
            
            var collectionQuery = queryFactory.CreateCollectionQuery(this.Options);
            var collection = collectionQuery.Execute();

            var buildDefinitionsQuery = queryFactory.CreateBuildDefinitionsQuery(collection, this.Options);
            var buildDefinitions = buildDefinitionsQuery.Execute();

            return buildDefinitions.Select(p => new
            {
                Name = p.Name
            });
        }
    }
}
