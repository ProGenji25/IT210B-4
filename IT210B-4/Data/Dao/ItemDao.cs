using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using IT210B_4.Models;

namespace IT210B_4.Data.Dao
{
    public class ItemDao : IItemDao
    {
        private readonly MongoDbContext _db;
        public ItemDao(IAtlasSettings settings)
        {
            _db = new MongoDbContext(settings);
        }
        public async Task Create(Item item)
        {
            try { await _db.Items.InsertOneAsync(item); }
            catch { throw; }
        }
        public async Task Delete(string id)
        {
            try
            {
                FilterDefinition<Item> data = Builders<Item>.Filter.Eq("Id", id);
                await _db.Items.DeleteOneAsync(data);
            }
            catch { throw; }
        }
        public async Task<Item> Get(string id)
        {
            try
            {
                FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", id);
                return await _db.Items.Find(filter).FirstOrDefaultAsync();
            }
            catch { throw; }
        }
        public async Task<IEnumerable<Item>> Read(string id)
        {
            try
            {
                FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("UserId", id);
                return await _db.Items.Find(filter).ToListAsync();
            }
            catch { throw; }
        }
        public async Task Update(Item item)
        {
            try { await _db.Items.ReplaceOneAsync(filter: g => g.Id == item.Id, replacement: item); }
            catch { throw; }
        }
    }

    public interface IItemDao
    {
        Task Create(Item item);
        Task Delete(string id);
        Task<Item> Get(string id);
        Task<IEnumerable<Item>> Read(string id);
        Task Update(Item item);
    }
}
