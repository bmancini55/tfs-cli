using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsCli.Models
{
    public class Build
    {
        public string Definition { get; set; }
        public string BuildNumber { get; set; }
        public string DropLocation { get; set; }
        public BuildStatus Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }
}
