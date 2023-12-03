using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;

namespace Timelogger
{
	public class ApiContext : DbContext
	{
        public DbSet<Project> Projects { get; set; }
        public DbSet<Interval> Intervals { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
			: base(options)
		{
		}


    }
}
