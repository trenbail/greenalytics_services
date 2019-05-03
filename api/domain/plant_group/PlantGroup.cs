using System;
using System.Collections.Generic;
using System.Linq;

namespace api.domain
{
    public class PlantGroup : Entity
    {
        public string Name { get; private set; }

        public List<Plant> Plants;
        private Hardware Hardware;
        private List<IPlantGroupStat> statistics;
        public Summary summary;

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
            return this.Hardware.Id;
        }

        public void AddPlant(Plant p)
        {
            this.Plants.Add(p);
        }

        public void DeletePlant(Plant p)
        {
            this.Plants.Remove(p);
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

    public class Summary
    {
        public readonly int temp;
        public readonly int humidity;

        public Summary(int temp, int humidity)
        {
            this.temp = temp;
            this.humidity = humidity;
        }

    }
}
