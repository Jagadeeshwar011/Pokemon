namespace PokeApi.Repositories;

using PokeApi.Entities;
using PokeApi.Models;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Create(User user);
    void Update(User user);
    void Delete(User user);

    LoginResponse IsAuthenticated(string userName, string password);

}