using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotesApp.Identity.DBContext.AuthDBContext;
using NotesApp.Identity.Models;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Authentication;

namespace NotesApp.Identity.DBContext.ApplicationDBContext;

public class ApplicationDBContext : IApplicationDBContext
{
    private readonly ApplicationDBConnectionDetails _connectionDetails;
    private readonly Lazy<MongoClient> _lazyMongoClient;

    public MongoClient MongoClient => _lazyMongoClient.Value;
    public IMongoDatabase MongoDatabase => MongoClient.GetDatabase(_connectionDetails.DataBase);

    public ApplicationDBContext(IOptions<ApplicationDBConnectionDetails> applicationDBConnectionDetails)
    {
        _connectionDetails = applicationDBConnectionDetails?.Value ??
            throw new ArgumentNullException(nameof(applicationDBConnectionDetails),
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
