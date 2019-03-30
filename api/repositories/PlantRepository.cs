using System;
using System.Collections.Generic;
using System.Data;
using api.domain;

namespace api.repositories
{
    public class PlantRepository : IPlantRepository
    {

        public void CreatePlant(Plant plant)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public Plant GetByName(string name)
        {
            Plant plant = new Plant(name);
            List<IPlantRequirement> requirements = new List<IPlantRequirement>(); //Get Requirements

            foreach(IPlantRequirement requirement in requirements)
            {
                plant.AddRequirement(requirement);
            }
            string description = ""; //get description

            plant.AddDescription(description);

            return plant;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
