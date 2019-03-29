using System;
using System.Collections.Generic;
using System.Linq;

namespace api.domain
{
    public class PlantGroup : Entity
    {
        public string Name { get; private set; }

        private List<Plant> Plants;
        private Hardware Hardware;
        private List<IPlantGroupStat> statistics;

        public PlantGroup(string name)
        {
            if (name == null || name == String.Empty)
            {
                throw new System.ArgumentException(nameof(name));
            }

            this.Name = name;
            this.Id = Guid.NewGuid();
            this.Plants = new List<Plant>();
        }
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

        public Guid getHardwareID()
        {
            throw new NotImplementedException();
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
