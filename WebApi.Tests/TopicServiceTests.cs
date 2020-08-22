// Unit testing controllers -- https://docs.microsoft.com/fi-fi/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

using Xunit;
using Moq;
using WebApi.Services;
using WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Topics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class TopicServiceTests
    {
        readonly List<List<Topic>> _data = new List<List<Topic>> {
            new List<Topic> { new Topic { Name = "BBB", Manager = new Account()}, },
            new List<Topic> { new Topic { Name = "BBB", Manager = new Account()}, new Topic { Name = "Conflict case", Manager = new Account() }, },
        };

        [Theory]
        [InlineData(ServiceResult.Ok, 0, "Success case", 0, true)]
        [InlineData(ServiceResult.Conflict, 0, "Conflict case", 1, true)]
        [InlineData(ServiceResult.UnAuthorized, 0, "Unauthorized case", 0, false)]
        public void TestTest(ServiceResult result, int accountId, string newTopicName, int dataCase, bool authorized)
        {
            var account = authorized ? new Account { Id = accountId } : null;
            var data = _data[dataCase].AsQueryable();

            var topicDbSet = new Mock<DbSet<Topic>>();
            topicDbSet.As<IQueryable<Topic>>().Setup(m => m.Provider).Returns(data.Provider);
            topicDbSet.As<IQueryable<Topic>>().Setup(m => m.Expression).Returns(data.Expression);
            topicDbSet.As<IQueryable<Topic>>().Setup(m => m.ElementType).Returns(data.ElementType);
            topicDbSet.As<IQueryable<Topic>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var accountDbSet = new Mock<DbSet<Account>>();
            var context = new Mock<DataContext>();
            context.Setup(m => m.Topics).Returns(topicDbSet.Object);
            context.Setup(m => m.Accounts).Returns(accountDbSet.Object);
            var service = new TopicService(context.Object);
            var request = new CreateTopicRequest { Name = newTopicName };

            Console.Write(newTopicName);
            var resultValue = service.Create(request, account);
            Assert.Equal(result, resultValue.ServiceResult);
            Console.WriteLine("...done.");
        }
    }
}