namespace PokeApi.Repositories;

using System.Collections.Generic;
using AutoMapper;
using PokeApi.Entities;
using PokeApi.Helpers;
using PokeApi.Models;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dbContext;

    public UserRepository(DataContext dbContext) {
        _dbContext = dbContext;
    }
    public void Create(User user)
    {
        if (_dbContext.Users.Any(x => x.Email == user.Email))
            throw new Exception( $"User with the email '{user.Email}' already exists");

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
    }

    public IEnumerable<User> GetAll()
    {
        return _dbContext.Users;
    }

    public User GetById(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
        if(user == null) throw new KeyNotFoundException("User not found!");
        return user;
    }

    public LoginResponse IsAuthenticated(string userName, string password)
    {
          var user = _dbContext.Users.FirstOrDefault(u => u.Email == userName);

        // validate
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Username or password is incorrect");
        var response = new LoginResponse { 
            Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Username = user.Email};

        return response;
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
    }
}