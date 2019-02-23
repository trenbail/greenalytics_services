using System;

namespace api.domain
{
    public class garden
    {
        public garden(string name, Guid gardenId, Guid accountId)
        {
            if (name == null || name == String.Empty)
            {
                throw new Exception<ArgumentException>(nameof(name));
            }
            if (gardenId == Guid.Empty)
            {
                throw new Exception<ArgumentException>(nameof(gardenId));
            }
            if (accountId == Guid.Empty)
            {
                throw new Exception<ArgumentException>(nameof(accountId));
            }
            this.name = name;
            this.GardenId = gardenId;
            this.AccountId = AccountId;
        }

        public garden(string name) : this(name, Guid.NewGuid(), Guid.NewGuid()) { }

        public Guid GardenId { get; private set; }
        public Guid AccountId { get; private set; }
        public string name { get; private set; }
        {
            
        }
    public void AddGardenGroup()
    {
    }
    public void GetStats()
    {

    }
}
}
