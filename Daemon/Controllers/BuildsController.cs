using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TfsCli.Models;
using TfsCli.Queries.Model;

namespace TfsCli.Daemon.Controllers
{
    public class BuildsController : BaseController
    {       
        [HttpGet]
        public IEnumerable<Build> GetBuilds(string collection, string project, string builddef)
        {
            var query = new BuildsQuery(this.TfsUri, collection, project, builddef);
            var results = query.Execute();
            return results;
        }

        [HttpGet]
        public Build GetLatestBuild(string collection, string project, string builddef, bool latest)
        {
            var query = new LastBuildQuery(this.TfsUri, collection, project, builddef);
            var result = query.Execute();
            return result;
        }
    }
}
