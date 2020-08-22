// Unit testing controllers -- https://docs.microsoft.com/fi-fi/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Projects;
using WebApi.Services;
using Xunit;

namespace UnitTests
{
    public class ProjectServiceTests
    {
        readonly List<List<Project>> _data = new List<List<Project>> {
            new List<Project> { new Project { Name = "BBB", Manager = new Account()}, },
            new List<Project> { new Project { Name = "BBB", Manager = new Account()}, new Project { Name = "Conflict case", Manager = new Account() }, },
        };

        [Theory]
        [InlineData(ServiceResult.Created, 0, "Success case", 0, true)]
        [InlineData(ServiceResult.Conflict, 0, "Conflict case", 1, true)]
        [InlineData(ServiceResult.UnAuthorized, 0, "Unauthorized case", 0, false)]
        public void CreateTest(ServiceResult result, int accountId, string newProjectName, int dataCase, bool authorized)
        {
            var account = authorized ? new Account { Id = accountId } : null;
            var data = _data[dataCase].AsQueryable();

            var context = mockDbSet(data);

            var service = new ProjectService(context.Object);
            var request = new CreateProjectRequest { Name = newProjectName };

            Console.Write(newProjectName);
            var resultValue = service.Create(request, account);
            Assert.Equal(result, resultValue.ServiceResult);
            Console.WriteLine("...done.");
        }

        [Theory]
        [InlineData(ServiceResult.NoContent, 0, "Success case", 0, true, 0)]
        [InlineData(ServiceResult.NotFound, 0, "Not Found case", 1, true, 5)]
        [InlineData(ServiceResult.UnAuthorized, 0, "Unauthorized case", 0, false, 0)]
        public void DeleteTest(ServiceResult result, int accountId, string newProjectName, int dataCase, bool authorized, int idToDelete)
        {
            var account = authorized ? new Account { Id = accountId } : null;
            var data = _data[dataCase].AsQueryable();

            var context = mockDbSet(data);

            var service = new ProjectService(context.Object);

            Console.Write(newProjectName);
            var resultValue = service.Delete(idToDelete, account);
            Assert.Equal(result, resultValue.ServiceResult);
            Console.WriteLine("...done.");
        }

        private Mock<DataContext> mockDbSet(IQueryable<Project> data)
        {
            var ProjectDbSet = new Mock<DbSet<Project>>();
            ProjectDbSet.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(data.Provider);
            ProjectDbSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(data.Expression);
            ProjectDbSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
            ProjectDbSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var accountDbSet = new Mock<DbSet<Account>>();
            var context = new Mock<DataContext>();
            context.Setup(m => m.Projects).Returns(ProjectDbSet.Object);
            context.Setup(m => m.Accounts).Returns(accountDbSet.Object);

            return context;
        }
    }
}