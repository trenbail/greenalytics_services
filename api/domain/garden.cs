using api.repositories;
using System;
using System.Collections.Generic;

namespace api.domain
{
    public class Garden : Entity
    {
        public Guid AccountId { get; private set; }
        public string Name {get; private set; }

        private List<PlantGroup> PlantGroups{get;}
        public Garden(string name, Guid accountId)
        {
            if (name == null || name == String.Empty)
            {
                throw new System.ArgumentException(nameof(name));
            }
            if (accountId == Guid.Empty)
            {
                throw new System.ArgumentException(nameof(accountId));
            }
            this.Name = name;
            this.Id = Guid.NewGuid();
            this.AccountId = AccountId;
        }

        public void AddGardenGroup()
        {
        }
        public void GetStats()
        {

        }
    }
}
