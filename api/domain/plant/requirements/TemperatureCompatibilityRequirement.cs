using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.domain.plant.requirements
{
    public class TemperatureCompatibilityRequirement : IPlantRequirement
    {
        bool IPlantRequirement.Verify(Plant plant1, Plant plant2)
        {
            throw new NotImplementedException();
        }
    }
}
