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
        public Build GetBuilds(string collection, string project, string buildDefinition)
        {            
            var query = new LastBuildQuery(this.TfsUri, collection, project, buildDefinition);
            var result = query.Execute();
            return result;
        }
    }
}
