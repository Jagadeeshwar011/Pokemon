namespace PokeApi.Entities;

public class Pokeman {
    public int Id {get;set;}

    public string Name {get;set;}

    public string Description {get;set;}

    public string Image {get;set;}

    public IList<UserFavorite> UserFavorites {get;set;}
}