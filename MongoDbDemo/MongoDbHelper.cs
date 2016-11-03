using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo
{
    public class MongodbHelper
    {
        public static readonly string Database = "DBA_UTKT";
        public static readonly string ConnectString = ConfigurationManager.ConnectionStrings["MongoTkt"].ConnectionString;

        private IMongoClient _client;
        private IMongoDatabase _database;

        /// <summary>
        /// create mongodb client with custom connction settings, and custom database
        /// </summary>
        /// <param name="connectString"> custom connction settings</param>
        /// <param name="databaseName">custom database</param>
        /// <returns>>mongodb helper instance<</returns>
        public static MongodbHelper CreateConnction(string connectString, string databaseName)
        {
            return new MongodbHelper(connectString, databaseName);
        }

        /// <summary>
        /// create mongodb client with custom connction settings
        /// </summary>
        /// <param name="connectString">custom connection string</param>
        /// <returns>>mongodb helper instance<</returns>
        public static MongodbHelper CreateConnction(string connectString)
        {
            return new MongodbHelper(connectString);
        }

        /// <summary>
        /// create mongodb client with default connction settings
        /// </summary>
        /// <returns>mongodb helper instance</returns>
        public static MongodbHelper CreateConnction()
        {
            return new MongodbHelper(MongodbHelper.ConnectString);
        }

        public MongodbHelper(string connectString, string assignDbName = "")
        {
            MongoUrl conn = new MongoUrl(connectString);
            string dbName = MongodbHelper.Database;
            if (!string.IsNullOrWhiteSpace(assignDbName)) dbName = assignDbName;
            else if (!string.IsNullOrWhiteSpace(conn.DatabaseName)) dbName = conn.DatabaseName;

            _client = new MongoClient(connectString);

            _database = _client.GetDatabase(dbName);
        }

        public async Task<bool> insertAync<T>(string collectionName, T source)
            where T: MongodbDocument, new()
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(collectionName);
            try
            {
                await collection.InsertOneAsync(source);
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            return true;
        }

        public async Task<List<T>> queryAsync<T>(string collectionName, Expression<Func<T, bool>> condition = null)
        {
            List<T> result = new List<T>();
            IMongoCollection<T> collection = _database.GetCollection<T>(collectionName);
            using (var cursor = await collection.FindAsync<T>(condition))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (T document in batch)
                    {
                        result.Add(document);
                    }
                }
            }
            return result;
        }

        public async Task<T> queryByIdAsync<T>(string collectionName, string id)
        {
            T result = default(T);

            IMongoCollection<T> collection = _database.GetCollection<T>(collectionName);
            var builder = Builders<T>.Filter;
            var filter = builder.Eq("_id", id);

            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (T document in batch)
                    {
                        result = document;
                    }
                }
            }
            return result;
        }

        public async Task<bool> updateAsync<T>(string collectionName, string lastModifiedDateField, Expression<Func<T, bool>> condition, List<MongodbParameter> updatePara)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(collectionName);

            var update = Builders<T>.Update;
            var updateDef = update.CurrentDate(lastModifiedDateField);
            foreach (MongodbParameter para in updatePara)
            {
                updateDef = updateDef.Set(para.key, para.value);
            }

            var result = await collection.UpdateManyAsync<T>(condition, updateDef);

            //collection.FindOneAndUpdateAsync<T>(condition)
            return result.ModifiedCount > 0;
        }

        public async Task<bool> updateByIdAsync<T>(string collectionName, ObjectId id, string lastModifiedDateField, IEnumerable<MongodbParameter> updatePara)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(collectionName);
            var update = Builders<T>.Update;
            var updateDef = update.CurrentDate(lastModifiedDateField);
            foreach (MongodbParameter para in updatePara)
            {
                updateDef = updateDef.Set(para.key, para.value);
            }
            
            var builder = Builders<T>.Filter;
            var filter = builder.Eq("_id", id);

            var result = await collection.FindOneAndUpdateAsync<T>(filter, updateDef);
            return result != null;
        }

        public async Task<bool> updateByIdAsync<T>(string collectionName, T source)
            where T : MongodbDocument, new()
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(collectionName);

            var builder = Builders<T>.Filter;
            var filter = builder.Eq("_id", source._id);

            var result = await collection.ReplaceOneAsync<T>(x=>x._id.Equals(source._id), source);
            return result != null;
        }

        public async Task<bool> deleteAsync<T>(string collectionName, Expression<Func<T, bool>> condition)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(collectionName);
            var result = await collection.DeleteManyAsync<T>(condition);
            return result.DeletedCount > 0;
        }

    }

    public abstract class MongodbDocument
    {
        [BsonId]
        public ObjectId _id { get; set; }
    }

    public class MongodbParameter
    {
        public object value { get; set; }
        public string key { get; set; }
    }
}
