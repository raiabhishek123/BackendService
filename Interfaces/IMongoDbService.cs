using backend_service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_service.Interfaces;

public interface IMongoDbService
{
    Task<List<User>> GetUser();
    Task<User> AddUser(User user);
    Task<User> DeleteUser(string id);
}