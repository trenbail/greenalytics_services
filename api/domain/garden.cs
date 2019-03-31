using api.repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.domain
{
    public class Garden : Entity
    {
        public string AccountId { get; private set; }
        public string Name {get; private set; }

        private List<PlantGroup> PlantGroups{get;}
        public Garden(string name, string accountId)
        {
            if (name == null || name == String.Empty)
            {
                throw new System.ArgumentException(nameof(name));
            }
            if (accountId == string.Empty)
            {
                throw new System.ArgumentException(nameof(accountId));
            }
            this.Name = name;
            this.Id = Guid.NewGuid();
            this.AccountId = AccountId;
            this.PlantGroups = new List<PlantGroup>();
        }

        public void AddPlantGroup(PlantGroup plantGroup)
        {
            this.PlantGroups.Add(plantGroup);
        }
        public PlantGroup GetPlantGroup(string name)
        {
            return this.PlantGroups.SingleOrDefault(pg => pg.Name == name);
        }
        public void GetStats()
        {

        }
    }
}
