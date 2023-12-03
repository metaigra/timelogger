using System;
using System.Collections.Generic;
using System.Text;
using Timelogger.Entities;
using Timelogger.Repositories;

namespace Timelogger.UseCases
{
    public class CreateProjectCase
    {
        private readonly ProjectRepository _repository;

        public CreateProjectCase(ProjectRepository repository)
        {
            _repository = repository;
        }

        public Project Exec(Project project)
        {
            return _repository.Create(project);
        }
    }
}
