using System;
using System.Collections.Generic;
using api.domain;
using db.sensors;
using Microsoft.AspNetCore.Mvc;

namespace api.repositories
{
    public class GardenRepository : IGardenRepository
    {
        public void CreateGarden(Garden garden)
        {
            //INSERT STATEMENT
            throw new NotImplementedException();
        }

        public void Delete()
        {
            
            throw new NotImplementedException();
        }

        public Garden GetByName(string name)
        {
            
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Update(Garden garden)
        {
            throw new NotImplementedException();
        }

        public void AddSensor()
        {
            Sensors sensor = new Sensors();
            sensor.CreateTable("xoxo");
        }

        public ActionResult<List<Garden>> GetAllGardens(string accountID)
        {
            throw new NotImplementedException();
        }
    }
}
