using Timelogger.Api.Controllers;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;
using System.Linq;
using AutoMapper;
using Timelogger.Api.DTO;
using Timelogger.UseCases;
using Timelogger.Repositories;
using Timelogger.Api.DTO.Map;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Timelogger.Api.Tests
{
    public class ProjectsControllerTests
    {
        private ProjectsController _contoller;

        [SetUp]
        public void SetUp() {
            var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase(databaseName: "InMemory")
            .Options;

            var db = new ApiContext(options);
            var mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingProfile>()
            );
            var mapper = mapperConfiguration.CreateMapper();

            var projectRepository = new ProjectRepository(db);

            _contoller = new ProjectsController(db, 
                mapper, 
                new CreateProjectCase(projectRepository),
                new GetProjectsCase(projectRepository));
        }

        [Test]
        public void Controller_AddProject_GetProject()
        {          
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            _contoller.Create(projectDto);
            var projects = (_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>;

            Assert.AreEqual(1, projects.Count);
            Assert.AreEqual(projectDto.Id, projects[0].Id);
            Assert.AreEqual(projectDto.Name, projects[0].Name);
        }
    }
}
