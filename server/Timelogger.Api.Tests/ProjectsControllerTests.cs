using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Timelogger.Api.DTO;
using System.Collections.Generic;
using System;
using Timelogger.UseCases.ProjectState.Const;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;
using Timelogger.Api.Tests.Lib;

namespace Timelogger.Api.Tests
{
    public class ProjectsControllerTests22
    {
        private HttpClient _httpClient;
        private ApiContext _db;

        [SetUp]
        public async Task Setup()
        {
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var existingDbContextService = services.SingleOrDefault(descriptor => descriptor.ServiceType == typeof(ApiContext));
                    if (existingDbContextService != null)
                        services.Remove(existingDbContextService);
                    services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("InMemory_" + Guid.NewGuid()));
                });
            });
            _httpClient = webHost.CreateClient();

            // Clean up DB
            _db = webHost.Services.CreateScope().ServiceProvider.GetRequiredService<ApiContext>();
            await _db.Database.EnsureDeletedAsync();
            await _db.Database.EnsureCreatedAsync();            
        }


        [Test]
        public async Task Controller_AddProject_GetProject()
        {
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            var postResponse = await _httpClient.Create("projects", projectDto);
            Assert.True(postResponse.IsSuccessStatusCode);

            var actualProjects = await _httpClient.Get<List<ProjectDto>>("projects");

            Assert.AreEqual(1, actualProjects.Count);
            Assert.AreEqual(projectDto.Id, actualProjects[0].Id);
            Assert.AreEqual(projectDto.Name, actualProjects[0].Name);
            Assert.AreEqual(States.STOP, actualProjects[0].State);
            Assert.AreEqual(0, actualProjects[0].Intervals.Count());
        }

        [Test]
        public async Task Controller_AddProjects_AssertCount()
        {
            var projects = new List<ProjectDto> {
                new ProjectDto() { Id = 1, Name = "Project 1" },
                new ProjectDto() { Id = 2, Name = "Project 2" }
            };

            await _httpClient.Create("projects", projects[0]);
            await _httpClient.Create("projects", projects[1]);

            var actual = await _httpClient.Get<List<ProjectDto>>("projects");

            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(projects[0].Id, actual[0].Id);
            Assert.AreEqual(projects[1].Id, actual[1].Id);
        }

        [Test]
        public async Task Conroller_StartProject()
        {
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            await _httpClient.Create("projects", projectDto);
            await _httpClient.Update("projects", new ProjectStateDto { Id = 1, State = States.START });

            var actual = (await _httpClient.Get<List<ProjectDto>>("projects"))[0];

            Assert.AreEqual(projectDto.Id, actual.Id);
            Assert.AreEqual(projectDto.Name, actual.Name);
            Assert.AreEqual(States.START, actual.State);
            Assert.AreEqual(1, actual.Intervals.Count());

            Assert.True(actual.Intervals.ToList()[0].Started.IsWithinLastMinute());
            Assert.IsNull(actual.Intervals.ToList()[0].Completed);
        }


        [Test]
        public async Task Controller_StopProject_ShortInterval()
        {
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            await _httpClient.Create("projects", projectDto);
            await _httpClient.Update("projects", new ProjectStateDto { Id = 1, State = States.START });
            await _httpClient.Update("projects", new ProjectStateDto { Id = 1, State = States.STOP });

            var actual = (await _httpClient.Get<List<ProjectDto>>("projects"))[0];

            Assert.AreEqual(States.STOP, actual.State);
            Assert.AreEqual(0, actual.Intervals.Count());
        }

        [Test]
        public async Task Controller_StopProject_IntervalMore30mins()
        {
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            await _httpClient.Create("projects", projectDto);
            await _httpClient.Update("projects", new ProjectStateDto { Id = 1, State = States.START });
            this.MoveStartIntervalDateTimeOneHourBack();
            await _httpClient.Update("projects", new ProjectStateDto { Id = 1, State = States.STOP });

            var actual = (await _httpClient.Get<List<ProjectDto>>("projects"))[0];

            Assert.AreEqual(States.STOP, actual.State);
            Assert.True(actual.Intervals.ToList()[0].Started.IsWithinLastMinute(IntervalConfig.MinimumIntervalInMinutes * 2));
            Assert.True(actual.Intervals.ToList()[0].Completed.Value.IsWithinLastMinute());
        }


        [Test]
        public async Task Controller_CompleteProject()
        {
            var projectDto = new ProjectDto { Id = 1, Name = "Project 1" };

            await _httpClient.Create("projects", projectDto);
            await _httpClient.Update("projects", new ProjectStateDto { Id = 1, State = States.START });
            this.MoveStartIntervalDateTimeOneHourBack();
            await _httpClient.Update("projects", new ProjectStateDto { Id = 1, State = States.COMPLETED });

            var actual = (await _httpClient.Get<List<ProjectDto>>("projects"))[0];

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
                interval.Started = interval.Started.AddMinutes(-IntervalConfig.MinimumIntervalInMinutes - 1);
            }

            _db.SaveChanges();
        }

    }
}