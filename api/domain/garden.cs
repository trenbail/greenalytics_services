using System;
using System.Collections.Generic;

namespace api.domain
{
    public class Garden
    {
        public Guid GardenId { get; private set; }
        public Guid AccountId { get; private set; }
        public List<PlantGroup> PlantGroups{get; private set;}
        public string Name {get; private set; }
        public Garden(string name, Guid gardenId, Guid accountId)
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

        public Garden(string name) : this(name, Guid.NewGuid(), Guid.NewGuid()) { }
    public void AddGardenGroup()
    {
    }
    public void GetStats()
    {

    }
}
}
