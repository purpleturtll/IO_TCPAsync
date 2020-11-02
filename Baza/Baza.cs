using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Server
{
    public class Baza
    {
        private MongoClient client;
        private IMongoDatabase db;
        private string dbName;

        public Baza(string dbName)
        {
            this.client = new MongoClient("mongodb://127.0.0.1:27017");
            this.db = client.GetDatabase("test");
            this.dbName = dbName;
        }
        public bool Select(string login, string password)
        {
            var res = db.GetCollection<BsonDocument>(this.dbName);
            var r = res.Find(new BsonDocument()).ToList();
            foreach (BsonDocument doc in r)
            {
                if (doc.GetElement("login").Value.ToString() == login && doc.GetElement("password").Value.ToString() == password) return true;
            }
            return false;
        }
    }
}
