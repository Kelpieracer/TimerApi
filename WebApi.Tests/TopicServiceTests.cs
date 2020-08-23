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
            var data = _data[dataCase];

            (var context, var mapper) = mockDbSet(data);

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

        [Theory]
        [InlineData(ErrorMessages.Code.Ok, 1, 0, true, 24)]
        [InlineData(ErrorMessages.Code.NotFound, 0, 1, true, 5)]
        [InlineData(ErrorMessages.Code.UnAuthorized, 0, 0, false, 0)]
        [InlineData(ErrorMessages.Code.UnAuthorized, 10, 0, true, 24)]
        public void DeleteTest(ErrorMessages.Code errorCode, int accountId, int dataCase, bool authorized, int idToDelete)
        {
            var account = authorized ? new Account { Id = accountId } : null;
            var data = _data[dataCase];

            (var context, var mapper) = mockDbSet(data);
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
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    Assert.Equal(count, context.Object.Topics.Count());
                    break;
                case ErrorMessages.Code.Conflict:
                    Assert.Equal(ErrorMessages.Text.GetValueOrDefault(errorCode), exception.Message);
                    Assert.Equal(count, context.Object.Topics.Count());
                    break;
            }

       }

        private (Mock<DataContext>, IMapper) mockDbSet(List<Topic> sourceList)
        {
            var topicDbSet = GetQueryableMockDbSet(sourceList);
            var accountDbSet = GetQueryableMockDbSet(new List<Account>());
            var context = new Mock<DataContext>();
            context.Setup(m => m.Topics).Returns(topicDbSet);
            context.Setup(m => m.Accounts).Returns(accountDbSet);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();

            return (context, mapper);
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            dbSet.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>((s) => sourceList.Remove(s));
            dbSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns((object[] oArray) => find(sourceList, oArray) as T);

            return dbSet.Object;
        }

        static object find(IEnumerable<object> oEnumerable, object[] keys)
        {
            // assumptions: primary key of object is named ID
            // primary key of object is an int
            // keys passed to .Find() method is a single value of int type
            foreach (var o in oEnumerable)
            {
                var t = o.GetType();
                var prop = t.GetProperty("ID");
                if (prop != null)
                {
                    if (prop.PropertyType == typeof(int))
                    {
                        if ((int)prop.GetValue(o) == (int)keys[0])
                        {
                            return o;
                        }
                    }
                }
            }
            return null;
        }
    }
}