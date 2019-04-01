using System;
using System.Collections.Generic;
using System.Data;
using api.domain;
using api.domain.plant.requirements;

namespace api.repositories
{
    public class PlantRepository : IPlantRepository
    {

        public void CreatePlant(Plant plant)
        {
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public List<Plant> GetAllPlants()
        {
        }

        public Plant GetByName(string name)
        {
            var plantDB = new db.plants.Plants();
            var plantID = plantDB.Convert(name);
            db.plants.PlantInfo plantInfo = plantDB.PlantData(plantID);


            List<IPlantRequirement> requirements = new List<IPlantRequirement>();
            requirements.Add(new SunlightCompatibilityRequirement(plantInfo.Sunlight));
            requirements.Add(new TemperatureCompatibilityRequirement(plantInfo.Temperature));
            requirements.Add(new WaterCompatibilityRequirement(plantInfo.Rainfall));

            //soil req
            var plantType = plantInfo.Type;
            var description = plantDB.Description(plantID);


            Plant plant = new Plant(name);
            plant.plantType = plantType;

            foreach(IPlantRequirement requirement in requirements)
            {
                plant.AddRequirement(requirement);
            }

            plant.AddDescription(description);

            return plant;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
