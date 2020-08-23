// Unit testing controllers -- https://docs.microsoft.com/fi-fi/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topics;
using WebApi.Services;
using WebApi.Tests.Mocks;
using Xunit;

namespace UnitTests
{
    public class TopicServiceTests
    {

        [Theory]
        [InlineData(ErrorMessages.Code.Ok, 0, "Success case", 0)]
        [InlineData(ErrorMessages.Code.Conflict, 1, "Conflict case", 1)]
        public void CreateTest(ErrorMessages.Code errorCode, int accountId, string newTopicName, int dataCase)
        {
            var account = new Account { Id = accountId };
            var data = CreateSampleData()[dataCase];

            (var context, var mapper) = MockDbSet.Create(data);

            var service = new TopicService(context.Object, mapper);
            var request = new CreateTopicRequest { Name = newTopicName };

            var count = context.Object.Topics.Count();
            TopicResponse resultValue = null;
            var exception = Record.Exception(() => { resultValue = service.Create(request, account); });
           
            switch (errorCode)
            {
                case ErrorMessages.Code.Ok:
                    Assert.NotNull(resultValue);
                    Assert.Null(exception);
                    Assert.Equal(count + 1, context.Object.Topics.Count());
                    break;
                case ErrorMessages.Code.UnAuthorized:
                case ErrorMessages.Code.Conflict:
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    Assert.Equal(count, context.Object.Topics.Count());
                    break;
                default:
                    throw new AppException("Unexpected exception");
            }
        }

        [Theory]
        [InlineData(ErrorMessages.Code.Ok, 24,  0)]
        [InlineData(ErrorMessages.Code.NotFound, 100, 1)]
        public void ReadTest(ErrorMessages.Code errorCode, int itemId, int dataCase)
        {
            var data = CreateSampleData()[dataCase];

            (var context, var mapper) = MockDbSet.Create(data);

            var service = new TopicService(context.Object, mapper);

            var count = context.Object.Topics.Count();
            TopicResponse resultValue = null;
            var exception = Record.Exception(() => { resultValue = service.Read(itemId); });
            Assert.Equal(count, context.Object.Topics.Count());

            switch (errorCode)
            {
                case ErrorMessages.Code.Ok:
                    Assert.NotNull(resultValue);
                    Assert.Null(exception);
                    break;
                case ErrorMessages.Code.NotFound:
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    break;
                default:
                    throw new AppException("Unexpected exception");
            }
        }

        [Theory]
        [InlineData(ErrorMessages.Code.Ok, 1, "Success case", 0, 24)]
        [InlineData(ErrorMessages.Code.NotFound, 1, "Conflict case", 1, 1000)]
        [InlineData(ErrorMessages.Code.NotFound, 1000, "Unauthorized case", 0, 24)]
        public void UpdateTest(ErrorMessages.Code errorCode, int accountId, string newTopicName, int dataCase, int idToUpdate)
        {
            var account = new Account { Id = accountId };
            var data = CreateSampleData()[dataCase];

            (var context, var mapper) = MockDbSet.Create(data);

            var service = new TopicService(context.Object, mapper);
            var request = new CreateTopicRequest { Name = newTopicName, Id = idToUpdate };

            var count = context.Object.Topics.Count();
            TopicResponse resultValue = null;
            var exception = Record.Exception(() => { resultValue = service.Update(request, account); });
            Assert.Equal(count, context.Object.Topics.Count());

            switch (errorCode)
            {
                case ErrorMessages.Code.Ok:
                    Assert.NotNull(resultValue);
                    Assert.Equal(newTopicName, resultValue.Name);
                    Assert.Equal(idToUpdate, resultValue.Id);
                    Assert.Null(exception);
                    break;
                case ErrorMessages.Code.UnAuthorized:
                case ErrorMessages.Code.NotFound:
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    Assert.Equal(count, context.Object.Topics.Count());
                    break;
                default:
                    throw new AppException("Unexpected exception");
            }
        }



        [Theory]
        [InlineData(ErrorMessages.Code.Ok, 1, 0, 24)]
        [InlineData(ErrorMessages.Code.NotFound, 0, 1, 5)]
        [InlineData(ErrorMessages.Code.UnAuthorized, 10, 0, 24)]
        public void DeleteTest(ErrorMessages.Code errorCode, int accountId, int dataCase,  int idToDelete)
        {
            var account = new Account { Id = accountId };
            var data = CreateSampleData()[dataCase];

            (var context, var mapper) = MockDbSet.Create(data);
            var service = new TopicService(context.Object, mapper);

            var count = context.Object.Topics.Count();
            var exception = Record.Exception(() => service.Delete(idToDelete, account));
            switch (errorCode)
            {
                case ErrorMessages.Code.Ok:
                    Assert.Null(exception);
                    Assert.Equal(count - 1, context.Object.Topics.Count());
                    break;
                case ErrorMessages.Code.UnAuthorized:
                case ErrorMessages.Code.NotFound:
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    Assert.Equal(count, context.Object.Topics.Count());
                    break;
                default:
                    throw new AppException("Unexpected exception");
            }
        }

        private List<List<Topic>> CreateSampleData() {
            return  new List<List<Topic>> {
                    new List<Topic> { new Topic { Name = "BBB", AccountId = 1, Id = 24}, },
                    new List<Topic> { new Topic { Name = "BBB", AccountId = 1, Id = 24 }, new Topic { Name = "Conflict case", AccountId = 1, Id = 25 }, },
        };
        }
    }
}