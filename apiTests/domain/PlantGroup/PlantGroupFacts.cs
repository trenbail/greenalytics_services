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
        public class TheAddPlantsConstructor : IDisposable
        {
            private PlantGroup _plantGroup;

            public TheAddPlantsConstructor()
            {
                _plantGroup = new PlantGroup("test plant group");
            }

            public class MockRequirement : IPlantRequirement
            {
                public bool ShouldFulfillRequirement { get; private set; }
                public MockRequirement(bool succeed)
                {
                    this.ShouldFulfillRequirement = succeed;
                }

                public bool Verify(Plant plant1, Plant plant2)
                {
                    return ShouldFulfillRequirement;
                }
            }

            [Fact]
            public void Test_AddPlantsANDEmptyPlants_ShouldReturnEmptyList()
            {
                _plantGroup.AddPlants(new Plant("test plant"));
            }
            [Fact]
            public void Test_AddPlantsWithPassingRequirements_ShouldReturnEmptyList()
            {
                Plant plant1 = new Plant("plant 1");
                plant1.AddRequirement(new MockRequirement(true));
                List<(Plant, List<IPlantRequirement>)> incompatiblePlants = _plantGroup.AddPlants(plant1);
                Assert.Empty(incompatiblePlants);
            }
            public void Dispose()
            {
            }
        }
    }
}
