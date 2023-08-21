using Microsoft.AspNetCore.Mvc;
using PokeApi.Logic;

namespace PokeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserFavoriteController : ControllerBase {
    private IUserFavoriteLogic _logic;

    public UserFavoriteController(IUserFavoriteLogic logic) {
        _logic = logic;
    }

    [HttpGet]
    public IActionResult GetAll(int userId)
    {
        var userFavs = _logic.GetAll(userId);
        return Ok(userFavs);
    }

    [HttpPost]
    public IActionResult Create(int userId, string pokemonName)
    {
        _logic.Create(userId, pokemonName);
        return Ok(new { message = "User favorite created" });
    }

    [HttpDelete]
    public IActionResult Remove(int userId, string pokemonName)
    {
        _logic.Remove(userId, pokemonName);
        return Ok(new { message = "User favorite removed" });
    }
}