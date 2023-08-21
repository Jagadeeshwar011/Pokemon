using AutoMapper;
using PokeApi.Entities;
using PokeApi.Models;
using PokeApi.Repositories;

namespace PokeApi.Logic;

public class PokemanLogic : IPokemanLogic
{
     private IPokemanRepository _repository;
    private IMapper _mapper;

    public PokemanLogic(IPokemanRepository repository, IMapper mapper){
        _repository = repository;
        _mapper = mapper;
    }

    public void Create(CreatePokeman request)
    {
        var pokeman = _mapper.Map<Pokeman>(request);

        _repository.Create(pokeman);
    }

    public IEnumerable<Pokeman> GetAll()
    {
        return _repository.GetAll();
    }

    public Pokeman GetById(int id)
    {
        return _repository.GetById(id);
    }
}