using AutoMapper;
using PokeApi.Entities;
using PokeApi.Models.Users;
using PokeApi.Repositories;

using BCrypt.Net;
using PokeApi.Models;

namespace PokeApi.Logic;

public class UserLogic : IUserLogic
{
    private IUserRepository _repository;
    private IMapper _mapper;

    public UserLogic(IUserRepository repository, IMapper mapper){
        _repository = repository;
        _mapper = mapper;
    }
    public void Create(CreateRequest model)
    {
        var user = _mapper.Map<User>(model);

        // hash password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
        _repository.Create(user);
    }

    public void Delete(int id)
    {
        var user = _repository.GetById(id);
        _repository.Delete(user);
    }

    public IEnumerable<User> GetAll()
    {
        return _repository.GetAll();
    }

    public User GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Update(int id, UpdateRequest model)
    {
        var user = _repository.GetById(id);
        // validate
        if (model.Email != user.Email && _repository.GetAll().Any(x => x.Email == model.Email))
            throw new Exception($"User with the email '{model.Email}' already exists");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

        // copy model to user and save
        _mapper.Map(model, user);
        _repository.Update(user);
    }
    public LoginResponse IsAuthenticated(LoginUser request) {
        return _repository.IsAuthenticated(request.Email, request.Password);
    }
}