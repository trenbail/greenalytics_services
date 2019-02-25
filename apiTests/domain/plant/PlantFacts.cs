using api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
            private Plant _plant1;
            private Plant _plant2;

            public TheReasonsForIncompatibilityMethod()
            {
                _plant1 = new Plant("test plant1");
                _plant2 = new Plant("test plant2");
            }

            public class MockRequirement : IPlantRequirement
            {
                public bool ShouldPass;
                public MockRequirement(bool shouldpass)
                {
                    this.ShouldPass = shouldpass;
                }

                public bool Verify(Plant plant1, Plant plant2)
                {
                    return ShouldPass;
                }
            }
            [Fact]
            public void Test_SinglePassingRequirement_ShouldReturnEmptyList()
            {
                _plant1.AddRequirement(new MockRequirement(true));
                List<IPlantRequirement> failed_requirements = _plant1.ReasonsForIncompatibility(_plant2);
                Assert.Empty(failed_requirements);
            }
            [Fact]
            public void Test_MultiplePassingRequirement_ShouldReturnEmptyList()
            {
                _plant1.AddRequirement(new MockRequirement(true));
                _plant1.AddRequirement(new MockRequirement(true));
                List<IPlantRequirement> failed_requirements = _plant1.ReasonsForIncompatibility(_plant2);
                Assert.Empty(failed_requirements);
            }
            [Fact]
            public void Test_FailingRequirement_ShouldReturnSingleElementList()
            {
                _plant1.AddRequirement(new MockRequirement(false));
                List<IPlantRequirement> failed_requirements = _plant1.ReasonsForIncompatibility(_plant2);
                Assert.Single(failed_requirements);
                List<MockRequirement> failed_mock_requirements = failed_requirements.Cast<MockRequirement>().ToList();
                Assert.False(failed_mock_requirements.First().ShouldPass);
            }
            [Fact]
            public void Test_MultipleFailingRequirement_ShouldReturnSingleElementList()
            {
                _plant1.AddRequirement(new MockRequirement(false));
                _plant1.AddRequirement(new MockRequirement(false));
                List<IPlantRequirement> failed_requirements = _plant1.ReasonsForIncompatibility(_plant2);
                Assert.NotEmpty(failed_requirements);
                Assert.True(failed_requirements.Count == 2);
                List<MockRequirement> failed_mock_requirements = failed_requirements.Cast<MockRequirement>().ToList();
                foreach (MockRequirement req in failed_requirements)
                {
                    Assert.False(req.ShouldPass);
                }
            }

            public void Dispose()
            {
            }
        }
    }
}
