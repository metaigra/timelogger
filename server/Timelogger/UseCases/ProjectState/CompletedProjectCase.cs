using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timelogger.Repositories;
using Timelogger.UseCases.ProjectState.Const;
using Timelogger.UseCases.ProjectState;

namespace Timelogger.UseCases
{
    public class CompletedProjectCase
    {
        private readonly ProjectRepository _repository;
        private readonly StopProjectCase _stopProjectCase;

        public CompletedProjectCase(ProjectRepository repository, StopProjectCase stopProjectCase)
        {
            this._repository = repository;
            this._stopProjectCase = stopProjectCase;
        }

        public void Exec(int projectId)
        {   
            var project = _repository.Get(projectId);
            if (project == null)
                return;
            if (project.State == States.START)
                _stopProjectCase.Exec(projectId);

            project.State = States.COMPLETED;
            _repository.Update(project);
        }

    }
}
