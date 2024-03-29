﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IT210B_4.Models;
using MongoDB.Driver;

namespace IT210B_4.Data
{
    public class MongoDbContext
    {
        private readonly IAtlasSettings _settings;
        private readonly IMongoDatabase _db;

        public MongoDbContext(IAtlasSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.ConnectionString);
            _db = client.GetDatabase(_settings.Database);
        }

        public IMongoCollection<Item> Items
        {
            get
            {
                return _db.GetCollection<Item>(_settings.Collection);
            }
        }
    }
}
