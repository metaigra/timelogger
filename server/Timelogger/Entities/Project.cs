using System.Collections.Generic;
using System.Threading.Tasks;

namespace Timelogger.Entities
{
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }

        public string State { get; set; }
        public string Deadline { get; set; }
        public ICollection<Interval> Intervals { get; set; }
    }
}
