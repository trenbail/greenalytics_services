﻿using api.domain;

namespace api.repositories
{
    public interface IPlantRepository : IRepository
    {
        Plant GetByName(string name);
        void CreatePlant(Plant plant);
    }
}