using System;
using System.Collections.Generic;
using System.Linq;

namespace api.domain
{
    public class PlantGroup
    {
        public string Name { get; private set; }
        public Guid PlantGroupId { get; private set; }
        public List<Plant> Plants{get; private set; }
        public Hardware Hardware { get; private set; }

        public PlantGroup(string name, Guid plantGroupId)
        {
            if (name == null || name == String.Empty)
            {
                throw new System.ArgumentException(nameof(name));
            }
            if (plantGroupId == Guid.Empty)
            {
                throw new System.ArgumentException(nameof(plantGroupId));
            }
            this.Name = name;
            this.PlantGroupId = plantGroupId;
            this.Plants = new List<Plant>();
        }
        public PlantGroup(string name) : this(name, Guid.NewGuid()) { }
        public List<(Plant, List<IPlantRequirement>)> GetAllIncompatibilities(Plant p)
        {
            List<(Plant, List<IPlantRequirement>)> incompatiblePlants = new List<(Plant, List<IPlantRequirement>)>();
            foreach(Plant plant in Plants)
            {
                List<IPlantRequirement> incompatibleWithPlant = plant.ReasonsForIncompatibility(p);
                if (incompatibleWithPlant.Any())
                {
                    incompatiblePlants.Add((plant, incompatibleWithPlant));
                }
            }
            return incompatiblePlants;
        }
        public void AddPlant(Plant p)
        {
            this.Plants.Add(p);
        }

        public void AddHardware(Hardware hw)
        {
            if(this.Hardware != null)
            {
                throw new ArgumentException(nameof(hw));
            }
            this.Hardware = hw ?? throw new ArgumentNullException(nameof(hw));
        }

        public IPlantGroupStat GetStats()
        {
            throw new NotImplementedException();
        }
        public List<PlantSchedule> Get_Schedule()
        {
            throw new NotImplementedException();
        }
        public void AddStats()
        {

        }
    }
}
