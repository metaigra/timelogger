using Timelogger.Api.Controllers;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Timelogger.Api.DTO;
using Timelogger.UseCases;
using Timelogger.Repositories;
using Timelogger.Api.DTO.Map;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;
using System;

namespace Timelogger.Api.Tests
{
    public class ProjectsControllerTests
    {
        private ProjectsController _contoller;

        [SetUp]
        public void SetUp() {

            var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase(databaseName: "InMemory_" + Guid.NewGuid())
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

        [Test]
        public void Controller_AddProjects_AssertCount()
        {
            int id1 = 1, id2 = 2;
            
            _contoller.Create(new ProjectDto() { Id = id1, Name = "Project 1" });
            _contoller.Create(new ProjectDto() { Id = id2, Name = "Project 2" });
            var projects = (_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>;

            Assert.AreEqual(2, projects.Count);
            Assert.AreEqual(id1, projects[0].Id);
            Assert.AreEqual(id2, projects[1].Id);
        }
    }
}
