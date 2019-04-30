using System.Collections.Generic;
using api.domain;
using Microsoft.AspNetCore.Mvc;

namespace api.repositories
{
    public interface IGardenRepository : IRepository
    {
        Garden GetByName(string name, string userID);
        void CreateGarden(Garden garden, string userID);
        void DeleteGarden(Garden garden, string userID);
        List<Garden> GetAllGardens(string accountID);
        void AddPlantGroup(Garden garden, PlantGroup plantGroup, string userID);
    }
}