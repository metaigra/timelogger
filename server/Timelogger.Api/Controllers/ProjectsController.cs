using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Timelogger.Api.DTO;
using Timelogger.Entities;
using Timelogger.UseCases;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	public class ProjectsController : Controller
	{
		private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly CreateProjectCase _createProjectCase;
        private readonly GetProjectsCase _getProjectsCase;

        public ProjectsController(ApiContext context,
            IMapper mapper, 
            CreateProjectCase createProjectCase, 
            GetProjectsCase getProjectsCase)
		{
			_context = context;
			_mapper = mapper;
			_createProjectCase = createProjectCase;
            _getProjectsCase = getProjectsCase;
        }

		[HttpGet]
		public IActionResult Get()
		{
            var projects = _getProjectsCase.Exec();
            var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
            return Ok(projectDtos);
		}

        public IActionResult Create([FromBody] ProjectDto projectDto)
        {
			var project = _mapper.Map<Project>(projectDto);
            var newProject = _createProjectCase.Exec(project);
            return Ok(newProject);
        }

    }
}
