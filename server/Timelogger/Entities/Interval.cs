using System;
using System.Collections.Generic;
using System.Text;

namespace Timelogger.Entities
{
    public class Interval
    {
        public int ProjectId { get; set; }
        public int? Started { get; set; }
        public int? Completed { get; set; }

    }
}
