using System;
using System.Collections.Generic;

namespace api.domain
{
    public class PlantGroup
    {
        public string Name { get; private set; }
        public Guid PlantGroupId { get; private set; }
        public Guid HardwareId { get; private set; }
        public List<Plant> Plants{get; private set; }
        public PlantGroup(string name, Guid plantGroupId, Guid HardwareId)
        {
            if (name == null || name == String.Empty)
            {
                throw new System.ArgumentException(nameof(name));
            }
            if (plantGroupId == Guid.Empty)
            {
                throw new System.ArgumentException(nameof(plantGroupId));
            }
            if (HardwareId == Guid.Empty)
            {
                throw new System.ArgumentException(nameof(HardwareId));
            }
            this.Name = name;
            this.PlantGroupId = plantGroupId;
            this.HardwareId = HardwareId;
        }
        public PlantGroup(string name) : this(name, Guid.NewGuid(), Guid.NewGuid()) { }
        public (Plant, List<PlantRequirement>) Add_plants(Plant p)
        {
            throw new NotImplementedException();
        }
        public void Add_Hardware()
        {

        }
        public PlantGroupStats Get_Stats()
        {
            throw new NotImplementedException();
        }
        public List<PlantSchedule> Get_Schedule()
        {
            throw new NotImplementedException();
        }
    }
}
