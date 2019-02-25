using api.domain;
using System;
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
                Assert.Throws<ArgumentException>(() => new Plant(null));
            }
            [Fact]
            public void Test_EmptyString_ShouldThrow()
            {
                Assert.Throws<ArgumentException>(() => new Plant(""));
            }
            public void Dispose()
            {
            }
        }
    }
}
