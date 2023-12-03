using System;
using System.Collections.Generic;
using System.Text;
using Timelogger.Entities;
using Timelogger.Repositories;
using Timelogger.UseCases.ProjectState.Const;

namespace Timelogger.UseCases.ProjectState
{
	public class StopProjectCase
	{
		private readonly ProjectRepository _repository;

		public StopProjectCase(ProjectRepository repository)
		{
			_repository = repository;
		}

		public void Exec(int projectId)
		{
			var project = _repository.Get(projectId);
			if (project == null)
				return;
			if (project.State != States.START)
				return;

			project.State = States.STOP;
			_repository.Update(project);
			_repository.StopInterval(project.Id);
			_repository.RemoveShortIntervals(project);
		}

	}
}
