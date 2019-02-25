using api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public class TheGetAllIncompatibilitiesMethod : IDisposable
        {
            private PlantGroup _plantGroup;

            public TheGetAllIncompatibilitiesMethod()
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
            public void Test_PlantWithPassingRequirement_ShouldReturnEmptyList()
            {
                Plant plant1 = new Plant("plant 1");
                Plant plant2 = new Plant("plant 2");
                plant1.AddRequirement(new MockRequirement(true));
                _plantGroup.AddPlant(plant1);
                List<(Plant, List<IPlantRequirement>)> incompatiblePlants = _plantGroup.GetAllIncompatibilities(plant2);
                Assert.Empty(incompatiblePlants);
            }
            [Fact]
            public void Test_AddPlantsWithFailingRequirements_ShouldReturnListWithPlan()
            {
                Plant plant1 = new Plant("plant 1");
                plant1.AddRequirement(new MockRequirement(false));
                _plantGroup.AddPlant(plant1);
                List<(Plant, List<IPlantRequirement>)> incompatiblePlants = _plantGroup.GetAllIncompatibilities(new Plant("plant 2"));
                Assert.Equal(incompatiblePlants.First().Item1.Name, plant1.Name);
                Assert.Single(incompatiblePlants);
            }
            [Fact]
            public void Test_AddPlantWithMixedRequirements_ShouldReturnListWithFailing()
            {
                Plant successPlant = new Plant("success plant");
                Plant failPlant = new Plant("fail plant");
                successPlant.AddRequirement(new MockRequirement(true));
                failPlant.AddRequirement(new MockRequirement(false));
                _plantGroup.AddPlant(successPlant);
                _plantGroup.AddPlant(failPlant);
                var incompatibleItems = _plantGroup.GetAllIncompatibilities(new Plant("plant 3"));
                Assert.Single(incompatibleItems);
                Assert.Equal(incompatibleItems.First().Item1.Name, failPlant.Name);
                Assert.False(incompatibleItems.First().Item2.Cast<MockRequirement>().First().ShouldFulfillRequirement); //The most terrible line of code I've ever written
                Assert.Single(incompatibleItems.First().Item2);
            }
            [Fact]
            public void Test_AddPlantWithMixedRequirementsANDMultipleFailingRequirements_ShouldReturnListWithFailing()
            {
                Plant successPlant = new Plant("success plant");
                Plant failPlant = new Plant("fail plant");
                successPlant.AddRequirement(new MockRequirement(true));
                failPlant.AddRequirement(new MockRequirement(false));
                failPlant.AddRequirement(new MockRequirement(false));
                _plantGroup.AddPlant(successPlant);
                _plantGroup.AddPlant(failPlant);
                var incompatibleItems = _plantGroup.GetAllIncompatibilities(new Plant("plant 3"));
                Assert.Single(incompatibleItems);
                Assert.Equal(incompatibleItems.First().Item1.Name, failPlant.Name);
                Assert.False(incompatibleItems.First().Item2.Cast<MockRequirement>().First().ShouldFulfillRequirement);
                Assert.Equal(2, incompatibleItems.First().Item2.Count);
            }
            [Fact]
            public void Test_AddPlantWithMultipleFailingRequirements_ShouldReturnListWithFailing()
            {
                Plant failPlant1 = new Plant("fail plant 1");
                Plant failPlant2 = new Plant("fail plant 2");
                failPlant1.AddRequirement(new MockRequirement(true));
                failPlant2.AddRequirement(new MockRequirement(true));
                failPlant1.AddRequirement(new MockRequirement(false));
                failPlant2.AddRequirement(new MockRequirement(false));
                _plantGroup.AddPlant(failPlant1);
                _plantGroup.AddPlant(failPlant2);
                var incompatibleItems = _plantGroup.GetAllIncompatibilities(new Plant("plant 3"));
                Assert.Equal(2, incompatibleItems.Count);
                Assert.Equal(incompatibleItems.First().Item1.Name, failPlant1.Name);
                Assert.Equal(incompatibleItems[1].Item1.Name, failPlant2.Name);

                Assert.False(incompatibleItems.First().Item2.Cast<MockRequirement>().First().ShouldFulfillRequirement);
                Assert.False(incompatibleItems[1].Item2.Cast<MockRequirement>().First().ShouldFulfillRequirement);
            }
            public void Dispose()
            {
            }
        }
        public class TheAddPlantMethod : IDisposable
        {
            private PlantGroup _plantGroup;

            public TheAddPlantMethod()
            {
                _plantGroup = new PlantGroup("test plant group");
            }

            public static IEnumerable<object[]> Data =>
                new List<Plant[]> {
                    new Plant[] { new Plant("plant 1") },
                    new Plant[] {new Plant("plant2") },
                    new Plant[] {new Plant("plant$$3") },
                    new Plant[] {new Plant("plant$$1"), new Plant("plant2") },
                };
            [Theory]
            [MemberData(nameof(Data))]
            public void Test_AddPlant_ShouldSucceced(params Plant[] plants)
            {
                foreach (Plant plant in plants)
                {
                    _plantGroup.AddPlant(plant);
                }
            }

            public void Dispose()
            {

            }
        }
        public class TheAddHardwareMethod : IDisposable
        {
            private PlantGroup _plantGroup;

            public TheAddHardwareMethod()
            {
                _plantGroup = new PlantGroup("test plant group");
            }

            [Fact]
            public void Test_AddHardware_ShouldSucceced()
            {
                _plantGroup.AddHardware(new Hardware());
            }

            [Fact]
            public void Test_AddHardware_HardwareAlreadyExists_ShouldThrow()
            {
                _plantGroup.AddHardware(new Hardware());
                Assert.Throws<ArgumentException>(() => _plantGroup.AddHardware(new Hardware()));
            }
            [Fact]
            public void Test_NullHardware_ShouldThrow()
            {
                Assert.Throws<ArgumentNullException>(() => _plantGroup.AddHardware(null));
            }
            public void Dispose()
            {

            }
        }
    }
}
