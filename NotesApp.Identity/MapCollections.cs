using MongoDB.Bson.Serialization;
using NotesApp.Domain;
using NotesApp.Domain.Common;

namespace NotesApp.Identity;

public static class MapCollections
{
    public static void MapAllCollections()
    {
        MapBaseEntity();
        MapUserCollection();
        MapUserCredentialsCollection();
    }

    private static void MapBaseEntity()
    {
        BsonClassMap.RegisterClassMap<BaseEnity>(x =>
        {
            x.AutoMap();
            x.SetIgnoreExtraElements(true);
            x.MapIdMember(x => x.ObjectId).SetIdGenerator(MongoDB.Bson.Serialization.IdGenerators.StringObjectIdGenerator.Instance);
        });
    }
    
    private static void MapUserCollection()
    {
        BsonClassMap.RegisterClassMap<User>(x =>
        {
            x.AutoMap();
            x.SetIgnoreExtraElements(true);
        });
    }    

    private static void MapUserCredentialsCollection()
    {
        BsonClassMap.RegisterClassMap<UserCredentials>(x =>
        {
            x.AutoMap();
            x.SetIgnoreExtraElements(true);
        });
    }
}
