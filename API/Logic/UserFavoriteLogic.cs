using PokeApi.Entities;
using PokeApi.Repositories;

namespace PokeApi.Logic;

public class UserFavoriteLogic : IUserFavoriteLogic
{
    private IUserFavoriteRepository _repository;

    public UserFavoriteLogic(IUserFavoriteRepository logic) {
        _repository = logic;
    }
    public void Create(int userId, string pokemonName)
    {
        _repository.Create(userId, pokemonName);
    }

    public IEnumerable<UserFavorite> GetAll(int userId)
    {
        return _repository.GetAll(userId);
    }

    public void Remove(int userId, string pokemonName)
    {
        _repository.Remove(userId, pokemonName);
    }
}