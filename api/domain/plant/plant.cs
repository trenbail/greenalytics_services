using System;
using System.Collections.Generic;

namespace api.domain
{
    public class plant
    {
        public string Name { get; private set; }
        //LOW, M, HIGH
        //enemies_of
        List<plant_requirement> requirements;

        public plant(string name)
        {
            if(name == null || name == String.Empty){
                throw new ArgumentException(nameof(name));
            }
            this.Name = name;
        }


        ///
        /// Returns a list of requirements taht are invalidated by the given plant
        ///
        public List<plant_requirement> ReasonsForIncompatibility(plant other_plant)
        {
            List<plant_requirement> failed_requirements = new List<plant_requirement>();
            foreach (plant_requirement req in requirements)
            {
                if (!req.Verify(this, other_plant)){
                    failed_requirements.Add(req);
                }
            }
            return failed_requirements;
        }
    }
}
