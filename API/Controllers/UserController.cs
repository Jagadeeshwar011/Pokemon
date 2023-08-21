using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Logic;
using PokeApi.Models;
using PokeApi.Models.Users;

namespace PokeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
    private IUserLogic _userLogic;
    private IMapper _mapper;

    public UserController(
        IUserLogic userLogic,
        IMapper mapper)
    {
        _userLogic = userLogic;
        _mapper = mapper;
    }

[HttpPost("authenticate")]
    public IActionResult Authenticate(LoginUser model)
    {
        var response = _userLogic.IsAuthenticated(model);
        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userLogic.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userLogic.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(CreateRequest model)
    {
        _userLogic.Create(model);
        return Ok(new { message = "User created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _userLogic.Update(id, model);
        return Ok(new { message = "User updated" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userLogic.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}