using PokeApi.Entities;
using PokeApi.Models;

namespace PokeApi.Logic;

public interface IPokemanLogic {
     IEnumerable<Pokeman> GetAll();
    Pokeman GetById(int id);

    void Create(CreatePokeman request);
}