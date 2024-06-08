using MongoDB.Driver;

namespace NotesApp.Identity.DBContext;

public interface IDBContext
{
    MongoClient MongoClient { get; }

    IMongoDatabase MongoDatabase { get; }

    IMongoCollection<T> GetMongoCollection<T>() where T : class;
}
