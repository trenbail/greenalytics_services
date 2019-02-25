using api.repositories;
using System;
using System.Collections.Generic;

namespace api.domain
{
    public class Garden : Entity
    {
        public Guid AccountId { get; private set; }

        private GardenRepository gardenRepository;

        public List<PlantGroup> PlantGroups{get; private set;}
        public string Name {get; private set; }
        public Garden(string name, Guid gardenId, Guid accountId, GardenRepository gardenRepository)
        {
            if (name == null || name == String.Empty)
            {
                throw new System.ArgumentException(nameof(name));
            }
            if (gardenId == Guid.Empty)
            {
                throw new System.ArgumentException(nameof(gardenId));
            }
            if (accountId == Guid.Empty)
            {
                throw new System.ArgumentException(nameof(accountId));
            }
            this.Name = name;
            this.Id = gardenId;
            this.AccountId = AccountId;
            this.gardenRepository = gardenRepository;
        }

        public Garden(string name, GardenRepository gardenRepository) : this(name, Guid.NewGuid(), Guid.NewGuid(), gardenRepository) { }
    public void AddGardenGroup()
    {
    }
    public void GetStats()
    {

    }
}
}
