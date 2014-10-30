﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Tfs_Cli.Daemon.Controllers
{
    public class BaseController : ApiController
    {        
        public Options Options
        {
            get { return this.Configuration.Properties["Options"] as Options; }
        }
    }
}
