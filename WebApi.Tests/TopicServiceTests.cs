// Unit testing controllers -- https://docs.microsoft.com/fi-fi/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

using AutoMapper;
using Moq;
using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topics;
using WebApi.Repositories;
using WebApi.Services;
using WebApi.Tests;
using Xunit;

namespace UnitTests
{
    public class TopicServiceTests
    {
        [Theory]
        [InlineData(ErrorMessages.Code.Ok, 0, "Success case")]
        [InlineData(ErrorMessages.Code.BadRequest, 0, "Null case")]
        public async void CreateTest(ErrorMessages.Code errorCode, int accountId, string newName)
        {
            var account = new Account { AccountId = accountId };
            var mockRepository = new Mock<ITopicRepository>();
            mockRepository.Setup((repo) => repo.AddAsync(It.IsAny<Topic>()))
                .ReturnsAsync((Topic entity) => entity);
            var mapper = MockMapper.GetNew();
            var service = new TopicService(mockRepository.Object, mapper);
            var request = errorCode == ErrorMessages.Code.Ok ? new CreateTopicRequest { Name = newName } : null;

            TopicResponse resultValue = null;
            var exception = await Record.ExceptionAsync(async () => { resultValue = await service.Create(request, account); });

            switch (errorCode)
            {
                case ErrorMessages.Code.Ok:
                    Assert.NotNull(resultValue);
                    Assert.Null(exception);
                    Assert.Equal(newName, resultValue.Name);
                    Assert.Equal(accountId, resultValue.AccountId);
                    Assert.Equal(0, resultValue.TopicId);
                    break;
                case ErrorMessages.Code.BadRequest:
                    Assert.NotNull(exception);
                    Assert.Null(resultValue);
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    break;
                default:
                    throw new AppException("Unexpected exception");
            }
        }

        [Theory]
        [InlineData(ErrorMessages.Code.Ok, 10, "Success case")]
        [InlineData(ErrorMessages.Code.BadRequest, 10, "Null case")]
        [InlineData(ErrorMessages.Code.UnAuthorized, 1000, "Null case")]
        public async void UpdateTest(ErrorMessages.Code errorCode, int accountId, string newName)
        {
            var entity = new Topic { Name = "Test", AccountId = accountId, TopicId = 1 };
            var mockRepository = new Mock<ITopicRepository>();
            mockRepository.Setup((repo) => repo.UpdateAsync(It.IsAny<Topic>()))
                .ReturnsAsync(() => entity);
            mockRepository.Setup((repo) => repo.FetchById(entity.TopicId))
                .Returns(() => entity);
            var mapper = MockMapper.GetNew();
            var service = new TopicService(mockRepository.Object, mapper);
            var request = errorCode != ErrorMessages.Code.BadRequest ? new UpdateTopicRequest { Name = newName, TopicId = 1 } : null;

            TopicResponse resultValue = null;
            var exception = await Record.ExceptionAsync(async () => { resultValue = await service.Update(request, accountId); });

            switch (errorCode)
            {
                case ErrorMessages.Code.Ok:
                    Assert.NotNull(resultValue);
                    Assert.Null(exception);
                    Assert.Equal(newName, resultValue.Name);
                    Assert.Equal(accountId, resultValue.AccountId);
                    Assert.Equal(1, resultValue.TopicId);
                    break;
                case ErrorMessages.Code.BadRequest:
                case ErrorMessages.Code.UnAuthorized:
                    Assert.NotNull(exception);
                    Assert.Null(resultValue);
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    break;
                default:
                    throw new AppException("Unexpected exception");
            }
        }

        [Theory]
        [InlineData(ErrorMessages.Code.Ok, 10, 1)]
        [InlineData(ErrorMessages.Code.BadRequest, 10, 1000)]
        [InlineData(ErrorMessages.Code.UnAuthorized, 1000, 1)]
        public async void DeleteTest(ErrorMessages.Code errorCode, int accountId, int id)
        {
            (var entity, var mockRepository, var mapper, var service) = GetSetup();
            mockRepository.Setup((repo) => repo.DeleteAsync(1, 10))
                .ReturnsAsync(() => entity);
            mockRepository.Setup((repo) => repo.FetchById(entity.TopicId))
                .Returns(() => entity);

            TopicResponse resultValue = null;
            var exception = await Record.ExceptionAsync(async () => { resultValue = await service.Delete(id, accountId); });

            switch (errorCode)
            {
                case ErrorMessages.Code.Ok:
                    Assert.NotNull(resultValue);
                    Assert.Null(exception);
                    Assert.Equal("Test", resultValue.Name);
                    Assert.Equal(1, resultValue.TopicId);
                    break;
                case ErrorMessages.Code.UnAuthorized:
                case ErrorMessages.Code.BadRequest:
                    Assert.NotNull(exception);
                    Assert.Null(resultValue);
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    break;
                default:
                    throw new AppException("Unexpected exception");
            }
        }

        (Topic, Mock<ITopicRepository>, IMapper, TopicService) GetSetup()
        {
            var entity = new Topic { Name = "Test", AccountId = 10, TopicId = 1 };
            var mockRepository = new Mock<ITopicRepository>();
            var mapper = MockMapper.GetNew();
            var service = new TopicService(mockRepository.Object, mapper);
            return (entity, mockRepository, mapper, service);
        }
    }
}