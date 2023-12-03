using System;
using System.Collections.Generic;
using System.Text;
using Timelogger.Entities;
using Timelogger.Repositories;
using Timelogger.UseCases.ProjectState.Const;

namespace Timelogger.UseCases.Project
{
    public class CreateProjectCase
    {
        private readonly ProjectRepository _repository;

        public CreateProjectCase(ProjectRepository repository)
        {
            _repository = repository;
        }

        public Timelogger.Entities.Project Exec(Timelogger.Entities.Project project)
        {
            project.State = States.STOP;
            return _repository.Create(project);
        }
    }
}
