﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsCli.Queries
{
    public interface IQuery<T>
    {
        T Execute();
    }
}
