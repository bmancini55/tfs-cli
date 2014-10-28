using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tfs_Cli.Queries;

namespace Tfs_Cli
{
    public class QueryFactory
    {       
        public CollectionQuery CreateCollectionQuery(Options options)
        {
            var query = new CollectionQuery();
            query.TfsUri = options.TfsUri;
            query.CollectionName = options.Collection;
            return query;
        }

        public BuildDefinitionsQuery CreateBuildDefinitionsQuery(TfsTeamProjectCollection collection, Options options)
        {
            var query = new BuildDefinitionsQuery();
            query.ProjectCollection = collection;
            query.Project = options.Project;
            query.BuildDefinition = options.BuildDefinition;
            return query;
        }

        public BuildsQuery CreateBuildsQuery(IBuildDefinition buildDefinition, Options options)
        {
            var query = new BuildsQuery();
            query.BuildDefinition = buildDefinition;
            return query;
        }
    }
}
