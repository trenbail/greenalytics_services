using System.Collections.Generic;
using api.domain;
using Microsoft.AspNetCore.Mvc;

namespace api.repositories
{
    public interface IGardenRepository : IRepository
    {
        Garden GetByName(string name);
        void CreateGarden(Garden garden);
        ActionResult<List<Garden>> GetAllGardens(string accountID);
        void AddPlantGroup(Garden garden, PlantGroup plantGroup, string userID);
    }
}