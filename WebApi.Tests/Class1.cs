using Xunit;
using Moq;
using WebApi.Services;
using WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;

namespace MyFirstUnitTests
{
    public class Class1
    {
        [Fact]
        public void TestTest()
        {
            var manager = new Mock<Account>();
            var topic = new Topic { Name = "Topic Namet", Manager = new Account() };
            var topicService = new Mock<ITopicService>();
            var context = new Mock<DataContext>();
            context.Setup(e => e.Topics.Add(topic)).Returns(topic);
            topicService.Setup()
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 3));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}