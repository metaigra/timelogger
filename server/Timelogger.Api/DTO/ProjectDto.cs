using System;
using System.Collections.Generic;

namespace Timelogger.Api.DTO
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Deadline { get; set; }

        public ICollection<IntervalDto> Intervals { get; set; }
    }

    public class IntervalDto
    {
        public int Id { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Completed { get; set; }
    }
}
