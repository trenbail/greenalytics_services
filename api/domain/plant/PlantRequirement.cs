using System;

namespace api.domain
{
    public class PlantRequirement
    {
        private Func<Plant, Plant, bool> req;

        public PlantRequirement(Func<Plant, Plant, bool> req) => this.req = req;
        public bool Verify(Plant plant1, Plant plant2){
            return req(plant1, plant2);
        }       
    }
}
