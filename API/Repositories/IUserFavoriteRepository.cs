using PokeApi.Entities;

namespace PokeApi.Repositories;

public interface IUserFavoriteRepository {
    public IEnumerable<UserFavorite> GetAll(int userId);

    public void Create(int userId, int pokemanId);

    public void Remove(int userId, int pokemanId);
}