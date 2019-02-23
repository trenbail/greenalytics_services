using System;
using System.Collections.Generic;

namespace api.domain
{
    public class plant_group
    {
        public string Name { get; private set; }
        public Guid PlantGroupId { get; private set; }
        public Guid HardwareId { get; private set; }
        public List<plant> Plants{get; private set; }
        public plant_group(string name, Guid plantGroupId, Guid HardwareId)
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
        public plant_group(string name) : this(name, Guid.NewGuid(), Guid.NewGuid()) { }
        public (plant, List<plant_requirement>) Add_plants(plant p)
        {
            throw new NotImplementedException();
        }
        public void Add_Hardware()
        {

        }
        public plant_group_stats Get_Stats()
        {
            throw new NotImplementedException();
        }
        public List<plant_schedule> Get_Schedule()
        {
            throw new NotImplementedException();
        }
    }
}
