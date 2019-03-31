using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using db.plants;

namespace api.domain.plant.requirements
{

    public class SunlightCompatibilityRequirement : IPlantRequirement
    {
        public sunlight_classes sunlightLevel;

        public SunlightCompatibilityRequirement(sunlight_classes sunlightLevel)
        {
            this.sunlightLevel = sunlightLevel;
        }

        public string TypeOfReq()
        {
            return "SUNLIGHT";
        }

        public bool Verify(Plant plant1, Plant plant2)
        {
            SunlightCompatibilityRequirement plant1_req = plant1.GetRequirement<SunlightCompatibilityRequirement>(typeof(SunlightCompatibilityRequirement));
            SunlightCompatibilityRequirement plant2_req = plant2.GetRequirement<SunlightCompatibilityRequirement>(typeof(SunlightCompatibilityRequirement));
            
            if(Math.Abs((int)plant1_req.sunlightLevel - (int)plant2_req.sunlightLevel) >= 2)
            {
                return false;
            }
            return true;
        }
    }
}
