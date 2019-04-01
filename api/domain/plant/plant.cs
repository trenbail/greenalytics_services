using System;
using System.Collections.Generic;

namespace api.domain
{
    public class Plant : ValueObject
    {
        public string Name { get; private set; }
        public List<IPlantRequirement> requirements;

        public string Description;
        private PlantSchedule schedule;
        public string plantType;
        public List<string> tempString;

        public Plant(string name)
        {
            if(name == null || name == String.Empty){
                throw new ArgumentException(nameof(name));
            }
            this.Name = name;
            this.requirements = new List<IPlantRequirement>();
        }


        ///
        /// Returns a list of requirements taht are invalidated by the given plant
        ///
        public List<IPlantRequirement> ReasonsForIncompatibility(Plant other_plant)
        {
            List<IPlantRequirement> failed_requirements = new List<IPlantRequirement>();
            foreach (IPlantRequirement req in requirements)
            {
                if (!req.Verify(this, other_plant)){
                    failed_requirements.Add(req);
                }
            }
            return failed_requirements;
        }

        public void AddRequirement(IPlantRequirement req)
        {
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            this.requirements.Add(req);
        }
        public T GetRequirement<T>(Type requirementType)
        {
            foreach(var requirement in this.requirements)
            {
                if(requirement.GetType() == requirementType)
                {
                    return (T)requirement;
                }
            }
            return default(T);
        }

        public void AddDescription(string desc)
        {
            this.Description = desc;
        }
    }
}
