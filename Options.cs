using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsCli
{
    public class Options
    {
        [Option('u', "uri", Required = true, HelpText = "TFS Server URI that will be connected to")]
        public string TfsUri { get; set; }

        [Option('a', "action", HelpText = "Action to be taken. Each action can use custom options.")]
        public string Action { get; set; }

        [Option('c', "collection", HelpText = "TFS Collection to query against")]
        public string Collection { get; set; }

        [Option('p', "project", HelpText = "Project inside of a collection to use")]
        public string Project { get; set; }

        [Option('b', "build", HelpText = "Build definition name to search for. Accepts wildcards")]
        public string BuildDefinition { get; set; }

        [Option('d', "Daemon", HelpText = "Start as a daemon that will accept entity requests")]
        public bool Daemon { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
