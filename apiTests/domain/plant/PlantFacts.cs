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
            [Theory]
            [InlineData("testplant")]
            [InlineData("test plant")]
            [InlineData("test$plant")]
            [InlineData("testplant123")]
            [InlineData("12321")]
            [InlineData("test_plant/123")]
            public void Test_NonEmptyStrings_NotThrow(string name)
            {
                new Plant(name);
            }
            public void Dispose()
            {
            }
        }
        public class TheAddRequirementMethod : IDisposable
        {
            private Plant _plant;

            public TheAddRequirementMethod()
            {
                _plant = new Plant("test plant");
            }

            public class MockPlantRequirement : IPlantRequirement
            {
                public bool Verify(Plant plant1, Plant plant2)
                {
                    throw new NotImplementedException();
                }
            }

            [Fact]
            public void Test_NewRequirement_AddRequirementMethod()
            {
                _plant.AddRequirement(new MockPlantRequirement());
            }
            [Fact]
            public void Test_NullRequirement_AddRequirementMethod()
            {
                Assert.Throws<ArgumentNullException>(
                    () =>_plant.AddRequirement(null));
            }

            public void Dispose()
            {
            }
        }
        public class TheReasonsForIncompatibilityMethod : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
