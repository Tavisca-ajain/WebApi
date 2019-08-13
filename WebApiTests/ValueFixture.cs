using System;
using Xunit;
using WebApi.Controllers;
using System.Collections.Generic;

namespace WebApiTests
{
    public class ValueFixture
    {
        [Fact]
        public void HelloTest()
        {
            var controller = new ValuesController();
            var actualresult = controller.Get("Hello").Value;
            Assert.Equal("Hey", actualresult);

        }
        [Fact]
        public void HeyTest()
        {
            var controller = new ValuesController();
            var actualresult = controller.Get("Hey").Value;
            Assert.Equal("Hello", actualresult);

        }
    }
}
