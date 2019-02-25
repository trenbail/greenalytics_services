using System;
using System.Collections.Generic;

namespace api.domain
{
    public class Plant
    {
        public string Name { get; private set; }
        private List<PlantRequirement> requirements;

        public Plant(string name)
        {
            if(name == null || name == String.Empty){
                throw new ArgumentException(nameof(name));
            }
            this.Name = name;
        }


        ///
        /// Returns a list of requirements taht are invalidated by the given plant
        ///
        public List<PlantRequirement> ReasonsForIncompatibility(Plant other_plant)
        {
            List<PlantRequirement> failed_requirements = new List<PlantRequirement>();
            foreach (PlantRequirement req in requirements)
            {
                if (!req.Verify(this, other_plant)){
                    failed_requirements.Add(req);
                }
            }
            return failed_requirements;
        }

        public void AddRequirement(PlantRequirement req)
        {
            this.requirements.Add(req);
        }
    }
}
