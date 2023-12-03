using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timelogger.Repositories;
using Timelogger.UseCases.ProjectState.Const;

namespace Timelogger.UseCases.ProjectState
{
    public class StartProjectCase
    {
        private readonly ProjectRepository _repository;
        private readonly StopProjectCase _stopProjectCase;

        public StartProjectCase(ProjectRepository repository, StopProjectCase stopProjectCase)
        {
            this._repository = repository;
            this._stopProjectCase = stopProjectCase;
        }

        public void Exec(int projectId)
        {
            var projects = _repository.GetAll().Where(p => p.State == States.START && projectId != p.Id).ToList();
            foreach (var currentProject in projects)
                _stopProjectCase.Exec(projectId);

            var project = _repository.Get(projectId);
            if (project == null)
                return;
            if (project.State == States.START)
                return;

            project.State = States.START;
            project.Intervals.Add(new Entities.Interval() { ProjectId = projectId, Started = DateTime.Now });
            _repository.Update(project);
        }

    }
}
