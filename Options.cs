using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tfs_Cli
{
    public class Options
    {
        [Option('a', "action", Required = true, HelpText = "Action to be taken. Each action can use custom options.")]
        public string Action { get; set; }

        [Option('u', "uri", Required=true, HelpText="TFS Server URI that will be connected to")]
        public string TfsUri { get; set; }

        [Option('c', "collection", Required = true, HelpText = "TFS Collection to query against")]
        public string Collection { get; set; }

        [Option('p', "project", Required = true, HelpText = "Project inside of a collection to use")]
        public string Project { get; set; }

        [Option('d', "build", HelpText = "Build definition name to search for. Accepts wildcards")]
        public string BuildDefinition { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
