using System;

namespace api.domain
{
    public class plant_group
    {
        public string Name { get; private set; }
        public Guid plantGroupId { get; private set; }
        public Guid HardwareId { get; private set; }
        public plant_group(string name, Guid plantGroupId, Guid HardwareId)
        {
            if (name == null || name = String.Empty)
            {
                throw new Exception<ArgumentException>(nameof(name));
            }
            if (plantGroupId == Guid.Empty)
            {
                throw new Exception<ArgumentException>(nameof(plantGroupId));
            }
            if (HardwareId == Guid.Empty)
            {
                throw new Exception<ArgumentException>(nameof(HardwareId));
            }
            this.Name = name;
            this.PlantGroupId = plantGroupId;
            this.HardwareId = HardwareId;
        }
        public plant_group(string name) : this(name, Guid.NewGuid(), Guid.NewGuid()) { }
        public (plant, list<plant_requirement>) Add_plants(plant p)
        {

        }
        public void Add_Hardware()
        {

        }
        public plant_group_stats Get_Stats()
        {

        }
        public void Get_Schedule()
        {

        }
    }
}
