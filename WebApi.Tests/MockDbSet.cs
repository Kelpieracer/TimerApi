using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Tests.Mocks
{
    static class MockDbSet
    {
        public static (Mock<DataContext>, IMapper) Create(List<Topic> sourceList)
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
            // assumptions: primary key of object is named Id
            // primary key of object is an int
            // keys passed to .Find() method is a single value of int type
            foreach (var o in oEnumerable)
            {
                var t = o.GetType();
                var prop = t.GetProperty("Id");
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
