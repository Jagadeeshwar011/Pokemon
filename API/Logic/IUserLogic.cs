using PokeApi.Entities;
using PokeApi.Models;
using PokeApi.Models.Users;

namespace PokeApi.Logic;

public interface IUserLogic {
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Create(CreateRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);

    LoginResponse IsAuthenticated(LoginUser request);
}