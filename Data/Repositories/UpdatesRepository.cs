using Data.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UpdatesRepository
    {
        private readonly ILiteDatabase _database;
        private ILiteCollection<UpdateItemEntity> _updates;

        public UpdatesRepository(ILiteDatabase database)
        {
            _database = database;

            if (!_database.CollectionExists("updates"))
            {
                _updates = _database.GetCollection<UpdateItemEntity>("updates");
                _updates.Insert(new UpdateItemEntity
                {
                    GameVersion = "0.00.0",
                    Version = 0,
                    Path = "dummypath",
                    Description = "Dummy description",
                });
                _updates.EnsureIndex(x => x.GameVersion);
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

        public async Task<List<UpdateItemEntity>> FetchUpdatesData()
        {
            return _updates.Query().ToList();
        }
    }
}
