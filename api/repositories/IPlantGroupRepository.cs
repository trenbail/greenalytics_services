using api.domain;

namespace api.repositories
{
    public interface IPlantGroupRepository
    {
        PlantGroup GetByName(string name);
        void CreatePlantGroup(PlantGroup plantGroup);
    }
}