using api.domain;

namespace api.repositories
{
    public interface IGardenRepository : IRepository
    {
        Garden GetByName(string name);
        void CreateGarden(Garden garden);
        void Update(Garden garden);
    }
}