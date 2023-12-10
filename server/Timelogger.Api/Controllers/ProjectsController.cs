using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml;
using Timelogger.Api.DTO;
using Timelogger.Entities;
using Timelogger.UseCases;
using Timelogger.UseCases.Project;
using Timelogger.UseCases.ProjectState;
using Timelogger.UseCases.ProjectState.Const;

namespace Timelogger.Api.Controllers
{
    [Route("api/[controller]")]
	public class ProjectsController : Controller
	{
		private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly CreateProjectCase _createProjectCase;
        private readonly GetProjectsCase _getProjectsCase;
        private readonly StartProjectCase _startProjectCase;
        private readonly StopProjectCase _stopProjectCase;
        private readonly CompletedProjectCase _completedProjectCase;

        public ProjectsController(ApiContext context,
            IMapper mapper, 
            CreateProjectCase createProjectCase, 
            GetProjectsCase getProjectsCase,
            StartProjectCase startProjectCase,
            StopProjectCase stopProjectCase,
            CompletedProjectCase completedProjectCase)
		{
            _context = context;
            _mapper = mapper;
            _createProjectCase = createProjectCase;
            _getProjectsCase = getProjectsCase;
            _startProjectCase = startProjectCase;
            _stopProjectCase = stopProjectCase;
            _completedProjectCase = completedProjectCase;
        }

		[HttpGet]
		public IActionResult Get()
		{
            var projects = _getProjectsCase.Exec();
            var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
            Console.Write(JsonConvert.SerializeObject(projectDtos, Newtonsoft.Json.Formatting.Indented));
            return Ok(projectDtos);
		}

        [HttpPost]
        public IActionResult Create([FromBody] ProjectDto projectDto)
        {
			var project = _mapper.Map<Project>(projectDto);
            var newProject = _createProjectCase.Exec(project);
            return Ok(newProject);
        }

        [HttpPatch]
        public IActionResult Update([FromBody] ProjectStateDto projectStateDto)
        {
            switch (projectStateDto.State)
            {
                case States.STOP:
                    _stopProjectCase.Exec(projectStateDto.Id);
                    break;
                case States.START:
                    _startProjectCase.Exec(projectStateDto.Id);
                    break;
                case States.COMPLETED:
                    _completedProjectCase.Exec(projectStateDto.Id);
                    break;
            }

            return Ok();
        }
    }
}
