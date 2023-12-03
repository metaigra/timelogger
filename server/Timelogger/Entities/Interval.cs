using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timelogger.Entities
{
    public class Interval
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Completed { get; set; }

        public Project Project { get; set; }
    }
}
