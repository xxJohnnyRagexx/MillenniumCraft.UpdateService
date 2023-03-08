using Data.Models;
using LiteDB;

namespace Data.Repositories
{
    public class UpdatesRepository : IUpdatesRepository
    {
        private readonly ILiteDatabase _database;
        private ILiteCollection<UpdateItemEntity> _updates;

        public UpdatesRepository(ILiteDatabase database)
        {
            _database = database;

            if (!_database.CollectionExists("updates"))
            {
                _updates = _database.GetCollection<UpdateItemEntity>("updates");
                var dummyEntity = new UpdateItemEntity
                {
                    GameVersion = "0.00.0",
                    Version = 0,
                    Path = "dummypath",
                    Description = "Dummy description",
                };
                _updates.Insert(dummyEntity);
                _updates.EnsureIndex(x => x.GameVersion);
                _updates.Delete(dummyEntity.Id);
                //code above needed because liteDB doesn't support creating indexes without data.
            }
            else _updates = _database.GetCollection<UpdateItemEntity>("updates");
        }

        public void AddNewUpdate(UpdateItemEntity entity)
        {
            var lastVersion = _updates.Query()
                .Where(x => x.GameVersion == entity.GameVersion)
                .OrderByDescending(x => x.Version)
                .FirstOrDefault();

            if (lastVersion != null)
            {
                lastVersion.Path = entity.Path;
                lastVersion.Version++;
                _updates.Update(lastVersion);
            }
            else
                _updates.Insert(entity);
        }

        public List<UpdateItemEntity> FetchUpdatesData()
        {
            return _updates.Query().ToList();
        }

        public UpdateItemEntity FetchUpdate(string version)
        {
            return _updates.Query()
                .Where(x => x.GameVersion == version)
                .OrderByDescending(x => x.Version)
                .FirstOrDefault();
        }
    }
}
