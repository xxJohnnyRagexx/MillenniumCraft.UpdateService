using Data.Models;

namespace Data.Repositories
{
    public interface IUpdatesRepository
    {
        void AddNewUpdate(UpdateItemEntity entity);
        UpdateItemEntity FetchUpdate(string version);
        List<UpdateItemEntity> FetchUpdatesData();
    }
}