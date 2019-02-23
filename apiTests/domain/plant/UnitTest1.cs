using System;
using api.domain;
using Xunit;

namespace apiTests
{
    public class PlantTests
    {
        public class ThePlantConstructor : IDisposable
        {

            [Fact]
            public void Test_NullName_ShouldThrow()
            {
                Assert.Throws<ArgumentException>(new Plant(null));
            }
            [Fact]
            public void Test2()
            {
            }
            public void Dispose()
            {
            }
        }
    }
}
