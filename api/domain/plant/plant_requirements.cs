using System;

namespace api.domain
{
    public class plant_requirement
    {
        private Func<plant, plant, bool> req;

        public plant_requirement(Func<plant, plant, bool> req) => this.req = req;
        public bool Verify(plant plant1, plant plant2){
            return req(plant1, plant2);
        }       
    }
}
