using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotesApp.Identity.Models;
using System.Security.Authentication;

namespace NotesApp.Identity.DBContext.AuthDBContext;

public class AuthDBContext : IAuthDBContext
{
    private readonly AuthDBConnectionDetails _connectionDetails;
    private readonly Lazy<MongoClient> _lazyMongoClient;

    public MongoClient MongoClient => _lazyMongoClient.Value;
    public IMongoDatabase MongoDatabase => MongoClient.GetDatabase(_connectionDetails.DataBase);

    public AuthDBContext(IOptions<AuthDBConnectionDetails> authDBConnectionDetails)
    {
        _connectionDetails = authDBConnectionDetails?.Value ??
            throw new ArgumentNullException(nameof(authDBConnectionDetails),
            "Database connection details cannot be null");

        _lazyMongoClient = new Lazy<MongoClient>(CreateMongoClient);
    }

    public IMongoCollection<T> GetMongoCollection<T>() where T : class
    {
        return MongoDatabase.GetCollection<T>(typeof(T).Name);
    }

    private MongoClient CreateMongoClient()
    {
        MongoClientSettings settings = new MongoClientSettings();
        settings.Server = new MongoServerAddress(_connectionDetails.Host,
            _connectionDetails.Port);

        settings.UseTls = false;
        settings.SslSettings = new SslSettings();
        settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

        MongoIdentity identity = new MongoInternalIdentity(_connectionDetails.AuthDataBase,
            _connectionDetails.UserName);
        MongoIdentityEvidence evidence = new PasswordEvidence(_connectionDetails.Password);

        settings.Credential = new MongoCredential(_connectionDetails.AuthMechanism,
            identity, evidence);

        return new MongoClient(settings);
    }
}
