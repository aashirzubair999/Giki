using ClassLibraryDAL;
using Model.Models;

namespace DataAccess.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUser(string email);
    
}