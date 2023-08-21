using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models;

public class CreatePokeman {
    [Required]
    public string Name {get;set;}

    public string Description {get;set;}

    public string Image {get;set;}
}