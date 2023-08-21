using Newtonsoft.Json;
using PagedList;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using static System.Net.WebRequestMethods;

namespace Pokemon.Controllers
{
    public class FavouriteController :AsyncController
    {
        // GET: Favourite
        public async Task<ActionResult> IndexAsync()
        {
            var userDetails = Session["userSession"];
            var details = Session["userSession"] as LoginResponse;
            string endpoint = "https://localhost:7113/UserFavorite?userId=" + details.Id;
            var listPoke = new List<PokemonModel>();
            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var pokemonListModel = JsonConvert.DeserializeObject<List<UserFavourite>>(await Response.Content.ReadAsStringAsync());
                       
                        foreach (var item in pokemonListModel)
                        {
                            var itemVal = new PokemonModel();
                            itemVal.Name = item.PokemonName;
                            listPoke.Add(itemVal);
                        }
                        

                        ViewData["pl"] = listPoke;
                        if (listPoke.Any())
                        {
                            string endpoint1 = "https://pokeapi.co/api/v2/pokemon?limit=100'";
                            using (HttpClient client1 = new HttpClient())
                            {
                                using (var Response1 = await client1.GetAsync(endpoint1))
                                {
                                    if (Response1.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var data1 = Response1.Content.ReadAsStringAsync();
                                        var listPK = new List<PokemonModel>();
                                        var reuslt = data1.Result;
                                        if (reuslt != null && reuslt.Length > 0)
                                        {
                                            var results = JsonConvert.DeserializeObject<RootObject>(reuslt);
                                            foreach (var item in results.results)
                                            {
                                                if (listPoke.Exists(p => p.Name == item.name))
                                                {
                                                    var pokedata = new PokemonModel();
                                                    pokedata.Name = item.name;
                                                    string url = item.url;
                                                    url = url.Replace("https://pokeapi.co/api/v2/pokemon/", "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/");

                                                    pokedata.url = $"{url.Substring(0, url.Length - 1)}.png";
                                                    // $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{i}.png";
                                                    // i++;
                                                    listPK.Add(pokedata);
                                                }


                                                // pokedata.Name = item

                                            }
                                        }
                                        // var pokemonListModel = JsonConvert.DeserializeObject<List<PokemonModel>>(await Response.Content.ReadAsStringAsync());


                                        TempData.Keep("pokemanList");
                                        return View(listPK);
                                        // return RedirectToAction("Home");
                                    }
                                    else
                                    {
                                        ModelState.Clear();
                                        ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                                        return View();
                                    }
                                }
                            }
                        }
                        return View();
                        // return RedirectToAction("Home");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                        return View();
                    }
                }
            }
            return View();
        }

        public async Task AddFavouriteAsync(string pokemonName)
        {
            var userDetails = Session["userSession"];
            if (userDetails != null)
            {
                var details = Session["userSession"] as LoginResponse;
                string endpoint = "https://localhost:7113/UserFavorite?userId=" + details.Id + "&pokemonName=" + pokemonName;
                using (HttpClient client = new HttpClient())
                {
                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var pokemonListModel = JsonConvert.DeserializeObject<List<UserFavourite>>(await Response.Content.ReadAsStringAsync());
                            if (pokemonListModel != null)
                            {
                                var checkExist = pokemonListModel.Find(x => x.PokemonName == pokemonName);
                                
                                    await RemoveFavAsync(details.Id, pokemonName);
                                ViewBag.favourite = pokemonListModel.Count - 1;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            /*
            
            if (TempData["Fav"] == null)
            {
                var ids = new List<int>();
                ids.Add(id);
                TempData["Fav"] = ids;
                TempData.Keep("Fav");
                ViewBag.favourite = ids.Count;
            }
            else
            {
                var list = TempData["Fav"] as IEnumerable<int>;
                if (list != null)
                {
                    if (list.Contains(id))
                    {
                        list = list.ToList();

                    }
                    else
                    {
                        var ids = new List<int>();
                        ids.Add(id);
                        TempData["Fav"] = ids;
                        TempData.Keep("Fav");
                        ViewBag.favourite = ids.Count;
                    }
                }
            }
            */

            // return Json("ok");
        }
        private async Task RemoveFavAsync(int userId, string pokemonName)
        {
            
            // UserFavorite?userId=7&pokemanId=1
            string endpoint = "https://localhost:7113/UserFavorite?userId=" + userId + "&pokemonName=" + pokemonName;
            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.DeleteAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                    }

                }
            }
        }

    }
}