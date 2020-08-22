// Unit testing controllers -- https://docs.microsoft.com/fi-fi/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topics;
using WebApi.Services;
using Xunit;

namespace UnitTests
{
    public class TopicServiceTests
    {
        readonly List<List<Topic>> _data = new List<List<Topic>> {
            new List<Topic> { new Topic { Name = "BBB", AccountId = 1, TopicId = 24}, },
            new List<Topic> { new Topic { Name = "BBB", AccountId = 1, TopicId = 24}, new Topic { Name = "Conflict case", AccountId = 1, TopicId = 25 }, },
        };

        [Theory]
        [InlineData(ErrorMessages.Code.Ok, 0, "Success case", 0, true)]
        [InlineData(ErrorMessages.Code.Conflict, 1, "Conflict case", 1, true)]
        [InlineData(ErrorMessages.Code.UnAuthorized, 0, "Unauthorized case", 0, false)]
        public void CreateTest(ErrorMessages.Code errorCode, int accountId, string newTopicName, int dataCase, bool authorized)
        {
            var account = authorized ? new Account { Id = accountId } : null;
            var data = _data[dataCase].AsQueryable();

            var context = mockDbSet(data);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();
            var service = new TopicService(context.Object, mapper);
            var request = new CreateTopicRequest { Name = newTopicName };

            TopicResponse resultValue = null;
            var exception = Record.Exception(() => { resultValue = service.Create(request, account); });
           
            switch (errorCode)
            {
                case ErrorMessages.Code.Ok:
                    Assert.NotNull(resultValue);
                    Assert.Null(exception);
                    break;
                case ErrorMessages.Code.UnAuthorized:
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    break;
                case ErrorMessages.Code.Conflict:
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    break;
            }
        }

        /*
        [Theory]
        [InlineData(ServiceResult.NoContent, 0, "Success case", 0, true, 0)]
        [InlineData(ServiceResult.NotFound, 0, "Not Found case", 1, true, 5)]
        [InlineData(ServiceResult.UnAuthorized, 0, "Unauthorized case", 0, false, 0)]
        public void DeleteTest(ServiceResult result, int accountId, string newTopicName, int dataCase, bool authorized, int idToDelete)
        {
            var account = authorized ? new Account { Id = accountId } : null;
            var data = _data[dataCase].AsQueryable();

            var context = mockDbSet(data);

            var service = new TopicService(context.Object);

            Console.Write(newTopicName);
            var resultValue = service.Delete(idToDelete, account);
            Assert.Equal(result, resultValue.ServiceResult);
            Console.WriteLine("...done.");
        }
        */
        private Mock<DataContext> mockDbSet(IQueryable<Topic> data)
        {
            var topicDbSet = new Mock<DbSet<Topic>>();
            topicDbSet.As<IQueryable<Topic>>().Setup(m => m.Provider).Returns(data.Provider);
            topicDbSet.As<IQueryable<Topic>>().Setup(m => m.Expression).Returns(data.Expression);
            topicDbSet.As<IQueryable<Topic>>().Setup(m => m.ElementType).Returns(data.ElementType);
            topicDbSet.As<IQueryable<Topic>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var accountDbSet = new Mock<DbSet<Account>>();
            var context = new Mock<DataContext>();
            context.Setup(m => m.Topics).Returns(topicDbSet.Object);
            context.Setup(m => m.Accounts).Returns(accountDbSet.Object);

            return context;
        }
    }
}