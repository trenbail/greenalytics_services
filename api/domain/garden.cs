using System;
using System.Collections.Generic;

namespace api.domain
{
    public class garden
    {
        public Guid GardenId { get; private set; }
        public Guid AccountId { get; private set; }
        public List<plant_group> PlantGroups{get; private set;}
        public string Name {get; private set; }
        public garden(string name, Guid gardenId, Guid accountId)
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
            this.GardenId = gardenId;
            this.AccountId = AccountId;
        }

        public garden(string name) : this(name, Guid.NewGuid(), Guid.NewGuid()) { }
    public void AddGardenGroup()
    {
    }
    public void GetStats()
    {

    }
}
}
