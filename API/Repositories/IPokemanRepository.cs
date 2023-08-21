using System.Collections.Generic;
using PokeApi.Entities;

namespace PokeApi.Repositories;

public interface IPokemanRepository {
    public IEnumerable<Pokeman> GetAll();
    public Pokeman GetById(int id);

    public void Create(Pokeman pokeman);
}
