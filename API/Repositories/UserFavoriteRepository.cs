using Microsoft.EntityFrameworkCore;
using PokeApi.Entities;
using PokeApi.Helpers;

namespace PokeApi.Repositories;

public class UserFavoriteRepository : IUserFavoriteRepository
{
    private readonly DataContext _dbContext;

    public UserFavoriteRepository(DataContext dbContext) {
        _dbContext = dbContext;
    }

    public void Create(int userId, string pokemonName)
    {
        if(_dbContext.UserFavorites.Any(uf => uf.UserId == userId && uf.PokemonName == pokemonName))
        throw new Exception( "User favorite already exists!");
        
        _dbContext.UserFavorites.Add(new UserFavorite { UserId = userId, PokemonName = pokemonName});
        _dbContext.SaveChanges();
    }

    public IEnumerable<UserFavorite> GetAll(int userId)
    {
        return _dbContext.UserFavorites.Where(uf => uf.UserId == userId)
        .Include(uf => uf.User);
    }

    public void Remove(int userId, string pokemonName)
    {
       var userFav = _dbContext.UserFavorites.FirstOrDefault(uf => uf.UserId == userId && uf.PokemonName == pokemonName);
       if(userFav != null) {
        _dbContext.UserFavorites.Remove(userFav);
        _dbContext.SaveChanges();
       }
    }
}