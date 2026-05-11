using System.Threading.Tasks;
using backend_service.Interfaces;
using backend_service.Models;
using MongoDB.Driver;

namespace backend_service.Services;

public class MongoDbService : IMongoDbService
{
    private readonly IMongoCollection<User> _mongoCollection;
    public MongoDbService(IConfiguration configuration)
    {
        var mongoDbSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
        var client = new MongoClient(mongoDbSettings?.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings?.DatabaseName);
        _mongoCollection = database.GetCollection<User>(mongoDbSettings?.CollectionName);
    }

    public async Task<List<User>> GetUser()
    {
        var response = await _mongoCollection.Find(_ => true).ToListAsync();
        return response;
    }
    public async Task<User> AddUser(User user)
    {
        await _mongoCollection.InsertOneAsync(user);
        return user;
    }
    public async Task<User> DeleteUser(string id)
    {
        var response = await _mongoCollection.FindOneAndDeleteAsync(u => u.Id == id);
        return response;
    }
}