using System;

namespace api.domain
{
    public class plant
    {
        public string Name { get; private set}
        //LOW, M, HIGH
        //enemies_of
        list<plant_requirements> requirements;


        ///
        /// Returns a list of requirements taht are invalidated by the given plant
        ///
        public list<plant_requirements> ReasonsForIncompatibility(plant other_plant){
            foreach(plant_requirement req in requirements){
                if(req.Verify(this, other_plant));
            }  
        }
    }
}
