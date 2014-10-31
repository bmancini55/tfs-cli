using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsCli.Models;

namespace TfsCli.Queries.Tfs
{
    public class TfsBuildsQuery : IQuery<IEnumerable<IBuildDetail>>
    {
        public TfsBuildsQuery(IBuildDefinition buildDefinition)
        {
            this.BuildDefinition = buildDefinition;
        }
        public IBuildDefinition BuildDefinition { get; set; }

        public IEnumerable<IBuildDetail> Execute()
        {
            return BuildDefinition.QueryBuilds();          
        }
    }
}
