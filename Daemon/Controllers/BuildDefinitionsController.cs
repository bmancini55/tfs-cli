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
    public class BuildDefinitionsController : BaseController
    {
        [HttpGet]
        public IEnumerable<BuildDefinition> GetAllBuildDefinitions(string collection, string project)
        {
            var query = new BuildDefinitionsQuery(this.TfsUri, collection, project, "*");
            var results = query.Execute();
            return results;
        }

        [HttpGet]
        public IEnumerable<BuildDefinition> GetBuildDefinitions(string collection, string project, string name)        
        {
            if (!name.EndsWith("*")) name += "*";
            var query = new BuildDefinitionsQuery(this.TfsUri, collection, project, name);
            var results = query.Execute();
            return results;
        }
    }
}
