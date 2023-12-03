using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timelogger.Entities;

namespace Timelogger.Repositories
{
    public class ProjectRepository
    {
        private readonly ApiContext _context;

        public ProjectRepository(ApiContext context)
        {
            _context = context;
        }

        public Project Create(Project project)
        {
            var newProject = _context.Projects.Add(project).Entity;
            _context.SaveChanges();
            return newProject;
        }
        
        public IList<Project> GetAll()
        {
            return _context
                .Projects
                .Include(p => p.Intervals)
                .ToList();
        }

        public Project? Get(int prjectId)
        {
            return _context.Projects
                .Include(p => p.Intervals)
                .FirstOrDefault(p => p.Id == prjectId);
        }

        internal void Update(Project project)
        {
            _context.SaveChanges();
        }

    }
}
