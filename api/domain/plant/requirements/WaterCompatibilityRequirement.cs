using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.domain.plant.requirements
{
    public class WaterCompatibilityRequirement : IPlantRequirement
    {
        float range = 0.2f;
        public float quantity;

        public WaterCompatibilityRequirement(float quantity)
        {
            this.quantity = quantity;
        }

        public bool Verify(Plant plant1, Plant plant2)
        {
            WaterCompatibilityRequirement plant1_req = plant1.GetRequirement<WaterCompatibilityRequirement>(typeof(WaterCompatibilityRequirement));
            WaterCompatibilityRequirement plant2_req = plant2.GetRequirement<WaterCompatibilityRequirement>(typeof(WaterCompatibilityRequirement));

            float m = new List<float> { plant1_req.quantity, plant2_req.quantity }.Max();
            float scaled1 = plant1_req.quantity / m;
            float scaled2 = plant2_req.quantity / m;

            if (Math.Abs(scaled1 - scaled2) > range)
            {
                return false;
            }
            return true;
        }

        public string TypeOfReq()
        {
            return "WATER";
        }
    }
}
