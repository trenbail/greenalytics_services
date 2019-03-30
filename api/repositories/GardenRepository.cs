using System;
using System.Collections.Generic;
using api.domain;
using db.sensors;
using Microsoft.AspNetCore.Mvc;

namespace api.repositories
{
    public class GardenRepository : IGardenRepository
    {
        public void AddPlantGroup(Garden garden)
        {
            throw new NotImplementedException();
        }

        public void CreateGarden(Garden garden)
        {
            throw new NotImplementedException();
        }

        public ActionResult<List<Garden>> GetAllGardens(string accountID)
        {
            throw new NotImplementedException();
        }

        public Garden GetByName(string name)
        {
            throw new NotImplementedException();
            //ifnexist throw exception
        }
    }
}
