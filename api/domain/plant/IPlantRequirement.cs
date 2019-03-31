using System;

namespace api.domain
{
    public interface IPlantRequirement
    {
        string TypeOfReq();
        bool Verify(Plant plant1, Plant plant2);      
    }
}
