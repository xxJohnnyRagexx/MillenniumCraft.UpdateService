using Data.Models;
using LiteDB;
using LiteDB.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UpdatesRepository : IUpdatesRepository
    {
        private readonly ILiteDatabaseAsync _database;
        private ILiteCollectionAsync<UpdateItemEntity> _updates;

        public UpdatesRepository(ILiteDatabaseAsync database)
        {
            _database = database;

            if (!_database.CollectionExistsAsync("updates").Result)
            {
                _updates = _database.GetCollection<UpdateItemEntity>("updates");
                _updates.InsertAsync(new UpdateItemEntity
                {
                    GameVersion = "0.00.0",
                    Version = 0,
                    Path = "dummypath",
                    Description = "Dummy description",
                });
                _updates.EnsureIndexAsync(x => x.GameVersion);
            }
            else _updates = _database.GetCollection<UpdateItemEntity>("updates");
        }

        public async Task AddNewUpdateAsync(UpdateItemEntity entity)
        {
            var lastVersion = await _updates.Query()
                .Where(x => x.GameVersion == entity.GameVersion)
                .OrderByDescending(x => x.Version)
                .FirstOrDefaultAsync();

            if (lastVersion != null)
            {
                lastVersion.Path = entity.Path;
                lastVersion.Version++;
                await _updates.UpdateAsync(lastVersion);
            }
            else
                await _updates.InsertAsync(entity);
        }

        public async Task<List<UpdateItemEntity>> FetchUpdatesData()
        {
            return await _updates.Query().ToListAsync();
        }

        public async Task<UpdateItemEntity> FetchUpdateAsync(string version)
        {
            return await _updates.Query().Where(x => x.GameVersion == version).OrderByDescending(x => x.Version).FirstOrDefaultAsync();
        }
    }
}
