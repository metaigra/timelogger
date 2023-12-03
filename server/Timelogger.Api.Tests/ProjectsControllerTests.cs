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
using Timelogger.UseCases.Project;
using Timelogger.UseCases.ProjectState.Const;
using System.Linq;

namespace Timelogger.Api.Tests
{
    public class ProjectsControllerTests
    {
        private ProjectsController _contoller;
        private ApiContext _db;

        [SetUp]
        public void SetUp() {

            var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase(databaseName: "InMemory_" + Guid.NewGuid())
            .Options;

            _db = new ApiContext(options);
            var mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingProfile>()
            );
            
            var mapper = mapperConfiguration.CreateMapper();

            var projectRepository = new ProjectRepository(_db);

            var stopProjectCase = new UseCases.ProjectState.StopProjectCase(projectRepository);
            _contoller = new ProjectsController(_db, 
                mapper, 
                new CreateProjectCase(projectRepository),
                new GetProjectsCase(projectRepository),                
                new UseCases.ProjectState.StartProjectCase(projectRepository, stopProjectCase),
                stopProjectCase,
                new CompletedProjectCase(projectRepository, stopProjectCase));
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
            Assert.AreEqual(States.STOP, projects[0].State);
            Assert.AreEqual(0, projects[0].Intervals.Count());
        }

        [Test]
        public void Controller_AddProjects_AssertCount()
        {
            var projects = new List<ProjectDto> {
                new ProjectDto() { Id = 1, Name = "Project 1" },
                new ProjectDto() { Id = 2, Name = "Project 2" }
            };

            _contoller.Create(projects[0]);
            _contoller.Create(projects[1]);
            var actual = (_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>;

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(projects[0].Id, actual[0].Id);
            Assert.AreEqual(projects[1].Id, actual[1].Id);
        }

        [Test]
        public void Controller_StartProject()
        {
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            _contoller.Create(projectDto);
            var projects = (_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>;

            _contoller.Update(new ProjectStateDto { Id = 1, State = States.START });
            var actual = ((_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>)[0];

            Assert.AreEqual(projectDto.Id, actual.Id);
            Assert.AreEqual(projectDto.Name, actual.Name);
            Assert.AreEqual(States.START, actual.State);
            Assert.AreEqual(1, actual.Intervals.Count());

            Assert.True(actual.Intervals.ToList()[0].Started.IsWithinLastMinute());
            Assert.IsNull(actual.Intervals.ToList()[0].Completed);
        }


        [Test]
        public void Controller_StopProject_ShortInterval()
        {
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            _contoller.Create(projectDto);
            var projects = (_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>;

            _contoller.Update(new ProjectStateDto { Id = 1, State = States.START });
            _contoller.Update(new ProjectStateDto { Id = 1, State = States.STOP });

            var actual = ((_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>)[0];

            Assert.AreEqual(States.STOP, actual.State);
            Assert.AreEqual(0, actual.Intervals.Count());
        }

        [Test]
        public void Controller_StopProject()
        {
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            _contoller.Create(projectDto);
            var projects = (_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>;

            _contoller.Update(new ProjectStateDto { Id = 1, State = States.START });
            this.MoveStartIntervalDateTimeOneHourBack();
            _contoller.Update(new ProjectStateDto { Id = 1, State = States.STOP });

            var actual = ((_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>)[0];

            Assert.AreEqual(States.STOP, actual.State);
            Assert.True(actual.Intervals.ToList()[0].Started.IsWithinLastMinute(IntervalConfig.MinimumIntervalInMinutes * 2));
            Assert.True(actual.Intervals.ToList()[0].Completed.Value.IsWithinLastMinute());
        }


        [Test]
        public void Controller_ComplateProject()
        {
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            _contoller.Create(projectDto);
            var projects = (_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>;

            _contoller.Update(new ProjectStateDto { Id = 1, State = States.START });
            this.MoveStartIntervalDateTimeOneHourBack();
            _contoller.Update(new ProjectStateDto { Id = 1, State = States.COMPLETED });
            var actual = ((_contoller.Get() as OkObjectResult).Value as IList<ProjectDto>)[0];

            Assert.AreEqual(States.COMPLETED, actual.State);
            Assert.True(actual.Intervals.ToList()[0].Started.IsWithinLastMinute(IntervalConfig.MinimumIntervalInMinutes * 2));
            Assert.True(actual.Intervals.ToList()[0].Completed.Value.IsWithinLastMinute());
        }

        private void MoveStartIntervalDateTimeOneHourBack()
        {
            var intervalsToUpdate = _db.Intervals.ToList();
            foreach (var interval in intervalsToUpdate)
            {
                if (interval.Started == null)
                    continue;
                interval.Started = interval.Started.AddMinutes(- IntervalConfig.MinimumIntervalInMinutes - 1);
            }

            _db.SaveChanges();
        }

    }
}


public static class DateTimeExtensions
{    public static bool IsWithinLastMinute(this DateTime dateTime, int toleranceMinutes = 1)
    {
        DateTime now = DateTime.Now;
        return now.AddMinutes(-toleranceMinutes) <= dateTime && dateTime <= now;
    }
}