using Data.Models;

namespace Data.Repositories
{
    public interface IUpdatesRepository
    {
        Task AddNewUpdateAsync(UpdateItemEntity entity);
        Task<UpdateItemEntity> FetchUpdateAsync(string version);
        Task<List<UpdateItemEntity>> FetchUpdatesData();
    }
}