using System.Security.Cryptography.X509Certificates;
using PokeApi.Entities;
using PokeApi.Helpers;

namespace PokeApi.Repositories;

public class PokemanRepository : IPokemanRepository
{
    private readonly DataContext _dbContext;

    public PokemanRepository(DataContext dbContext) {
        _dbContext = dbContext;
    }

    public void Create(Pokeman pokeman)
    {
        if (_dbContext.Pokemen.Any(x => x.Name == pokeman.Name))
            throw new Exception( $"Pokeman '{pokeman.Name}' already exists");

        _dbContext.Pokemen.Add(pokeman);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Pokeman> GetAll()
    {
        return _dbContext.Pokemen;
    }

    public Pokeman GetById(int id)
    {
         var pokeman = _dbContext.Pokemen.FirstOrDefault(p => p.Id == id);
        if(pokeman == null) throw new KeyNotFoundException("Pokeman not found!");
        return pokeman;
    }
}