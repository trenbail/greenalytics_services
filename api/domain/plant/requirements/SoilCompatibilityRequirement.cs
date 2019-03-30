using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.domain.plant.requirements
{

    public class SoilCompatibilityRequirement : IPlantRequirement
    {
        public bool Verify(Plant plant1, Plant plant2)
        {
            throw new NotImplementedException();
        }
    }
}