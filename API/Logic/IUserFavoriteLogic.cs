using PokeApi.Entities;

namespace PokeApi.Logic;

public interface IUserFavoriteLogic {
    public IEnumerable<UserFavorite> GetAll(int userId);

    public void Create(int userId, string pokemonName);
    
    public void Remove(int userId, string pokemonName);
}