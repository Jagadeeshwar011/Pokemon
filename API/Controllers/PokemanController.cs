using Microsoft.AspNetCore.Mvc;
using PokeApi.Logic;
using PokeApi.Models;

namespace PokeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemanController : ControllerBase {
    private IPokemanLogic _pokemanLogic;

    public PokemanController(
        IPokemanLogic pokemanLogic)
    {
        _pokemanLogic = pokemanLogic;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var pokemen = _pokemanLogic.GetAll();
        return Ok(pokemen);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var pokeman = _pokemanLogic.GetById(id);
        return Ok(pokeman);
    }

    [HttpPost]
    public IActionResult Create(CreatePokeman model)
    {
        _pokemanLogic.Create(model);
        return Ok(new { message = "Pokeman created" });
    }
}