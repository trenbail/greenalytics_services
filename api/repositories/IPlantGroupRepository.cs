using api.domain;
using System;
using System.Collections.Generic;

namespace api.repositories
{
    public interface IPlantGroupRepository : IRepository
    {
        PlantGroup GetByName(string name, string userID);
        void CreatePlantGroup(Garden garden, PlantGroup plantGroup, string userID);
        void AddPlantToPlantGroup(PlantGroup plantGroup, Plant plant, string userID);
        void DeletePlantFromPlantGroup(PlantGroup plantGroup, Plant plant, string userID);
        List<(string, string)> GatherWaterNotifications(DateTime now);
    }
}