using db.plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.domain.plant.requirements
{
    
    public class TemperatureCompatibilityRequirement : IPlantRequirement
    {
        public temperature_classes temperature_Classes;

        public TemperatureCompatibilityRequirement(temperature_classes temperature_Classes)
        {
            this.temperature_Classes = temperature_Classes;
        }

        bool IPlantRequirement.Verify(Plant plant1, Plant plant2)
        {
            var plant1_req = plant1.GetRequirement<TemperatureCompatibilityRequirement>(typeof(TemperatureCompatibilityRequirement));
            var plant2_req = plant2.GetRequirement<TemperatureCompatibilityRequirement>(typeof(TemperatureCompatibilityRequirement));

            if (Math.Abs((int)plant1_req.temperature_Classes - (int)plant2_req.temperature_Classes) >= 2)
            {
                return false;
            }
            return true;
        }
    }
}
