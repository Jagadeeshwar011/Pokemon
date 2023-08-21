namespace PokeApi.Entities;

public class UserFavorite {
     public int Id {get;set;}

     public int UserId {get;set;}

     public User User {get;set;}

     public string PokemonName {get;set;}

}
