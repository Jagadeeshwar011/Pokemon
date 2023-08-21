namespace PokeApi.Helpers;

using Microsoft.EntityFrameworkCore;
using PokeApi.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlite(Configuration.GetConnectionString("PokeDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<UserFavorite>().HasKey(uf => new { uf.UserId, uf.PokemonName} );

        modelBuilder.Entity<UserFavorite>()
            .HasOne<User>(uf=> uf.User)
            .WithMany(u => u.UserFavorites)
            .HasForeignKey(uf => uf.UserId);


        modelBuilder.Entity<User>().HasData(
            new User{ Id = 1, FirstName = "Hariharasudhan", LastName = "Chandramurthy", Role = Role.Admin, 
            Email = "hspharic@gmail.com", PasswordHash = "$2a$11$FKBUZcWA0UfmRbWwGVFyheLGHcLEPl4ox8.P61eiHWiYW7JZ8vuka"},
            new User{ Id = 2, FirstName = "Karthikkannan", LastName = "Marimuthu", Role = Role.Admin, 
            Email = "karthikkannanmari@gmail.com", PasswordHash = "$2a$11$FKBUZcWA0UfmRbWwGVFyheLGHcLEPl4ox8.P61eiHWiYW7JZ8vuka"},
            new User{ Id = 3, FirstName = "Admin", LastName = "Admin", Role = Role.Admin, 
            Email = "admin@admin.com", PasswordHash = "$2a$11$ShZyQTkna2/BBB41B8Qdf.aRl4lPVRCI8NLZ14jesg9J8Sl8XSu0K"}
        );

        
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Pokeman> Pokemen {get;set;}

    public DbSet<UserFavorite>  UserFavorites {get;set;}
}