using System;
using System.Collections.Generic;
using System.Linq;
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
			foreach(var interval in project.Intervals)
			{
				if (interval.Completed != null)
					continue;

				interval.Completed = DateTime.Now;
			}

			project.Intervals = project
				.Intervals
				.Where(i => (i.Completed - i.Started).Value.TotalMinutes >= IntervalConfig.MinimumIntervalInMinutes)
				.ToList();

			_repository.Update(project);
		}

	}
}
