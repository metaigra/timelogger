using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timelogger.Entities;
using Timelogger.Repositories;

namespace Timelogger.UseCases.Project
{
    public class GetProjectsCase
    {
        private readonly ProjectRepository _repository;

        public GetProjectsCase(ProjectRepository repository)
        {
            _repository = repository;
        }

        public IList<Timelogger.Entities.Project> Exec()
        {
            return _repository.GetAll().ToList();
        }
    }
}
