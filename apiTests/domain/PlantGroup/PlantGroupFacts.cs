using api.domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace apiTests.domain
{
    public class PlantGroupFacts
    {
        public class ThePlantGroupConstructor : IDisposable
        {
            [Fact]
            public void Test_NullName_ShouldThrow()
            {
                Assert.Throws<ArgumentException>(() => new PlantGroup(null));
            }
            [Fact]
            public void Test_EmptyName_ShouldThrow()
            {
                Assert.Throws<ArgumentException>(() => new PlantGroup(""));
            }
            [Fact]
            public void Test_EmptyGuid_ShouldThrow()
            {
                Assert.Throws<ArgumentException>(() => new PlantGroup("name", Guid.Empty));
            }
            public void Dispose()
            {
            }
        }
    }
}
