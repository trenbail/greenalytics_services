using System;

namespace api.domain
{
    public interface IPlantRequirement
    {

        bool Verify(Plant plant1, Plant plant2);      
    }
}
