using System;
using System.Collections.Generic;
using System.Linq;

namespace api.domain
{
    public class PlantGroup
    {
        public string Name { get; private set; }
        public Guid PlantGroupId { get; private set; }
        public Guid HardwareId { get; private set; }
        public List<Plant> Plants{get; private set; }
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
        public void Add_Hardware()
        {

        }
        public IPlantGroupStat Get_Stats()
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
