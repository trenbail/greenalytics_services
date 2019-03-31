using api.domain;
using System;

namespace api.repositories
{
    public interface IPlantGroupRepository
    {
        PlantGroup GetByName(string name, string userID);
        void CreatePlantGroup(Garden garden, PlantGroup plantGroup, string userID);
        void AddPlantToPlantGroup(PlantGroup plantGroup, Plant plant, string userID);
    }
}