using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tfs_Cli.Queries
{
    public class BuildsQuery : IQuery<List<IBuildDetail>>
    {
        public IBuildDefinition BuildDefinition { get; set; }

        public List<IBuildDetail> Execute()
        {
            return BuildDefinition.QueryBuilds().ToList();          
        }
    }
}
