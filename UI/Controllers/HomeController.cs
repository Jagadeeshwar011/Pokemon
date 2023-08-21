using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PagedList;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pokemon.Controllers
{
    public class HomeController : AsyncController
    {
        public async Task<ActionResult> IndexAsync(int? page)
        {
            if (TempData["Fav"] != null)
            {
                var count = TempData["Fav"] as List<int>;
                ViewBag.favourite = count.Count;
            }
            else
            {
                ViewBag.favourite = "0";
            }
           
            if (TempData["userSession"] != null)
            {
                ViewBag.firstname = TempData["userName"];
            }
            string endpoint = "https://pokeapi.co/api/v2/pokemon?limit=100'";
            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var data1 = Response.Content.ReadAsStringAsync();
                        var listPK = new List<PokemonModel>();
                        var reuslt = data1.Result;
                        if (reuslt != null && reuslt.Length > 0)
                        {
                            var results = JsonConvert.DeserializeObject<RootObject>(reuslt);
                            foreach (var item in results.results)
                            {
                                var pokedata = new PokemonModel();
                                 pokedata.Name = item.name;
                                 string url = item.url;
                                url = url.Replace("https://pokeapi.co/api/v2/pokemon/", "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/");

                                pokedata.url =  $"{url.Substring(0, url.Length - 1)}.png";
                                    // $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{i}.png";
                                // i++;
                                listPK.Add(pokedata);
                                // pokedata.Name = item

                            }
                        }
                        // var pokemonListModel = JsonConvert.DeserializeObject<List<PokemonModel>>(await Response.Content.ReadAsStringAsync());
                        var data = listPK.ToPagedList(page ?? 1, 5);
                        var count = data.Count;
                        TempData["pokemanList"] = data;

                        TempData.Keep("pokemanList");
                        ViewData["pl"] = data;
                        return View((listPK.ToPagedList(page ?? 1, 5)));
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
            


            // return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public void CheckFavourite(string data)
        {
            ViewBag.Message = "Your application description page.";
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(string option, string search, int? page)
        {
            /*
            if (!string.IsNullOrEmpty(search))
            {
                ViewData["error"] = "";
                string endpoint = "https://localhost:7113/Pokeman";
                using (HttpClient client = new HttpClient())
                {
                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var pokemonListModel = JsonConvert.DeserializeObject<List<PokemonModel>>(await Response.Content.ReadAsStringAsync());
                            TempData["pokemanList"] = pokemonListModel;
                            TempData.Keep("pokemanList");
                            //ViewData["pl"] = pokemonListModel;
                           
                            // return RedirectToAction("Home");
                        }
                    }
                }
                var list = TempData["pokemanList"] as List<PokemonModel>;
                search = search.ToLower().Trim();
                var filter = list.Find(x => x.Name.ToLower().Trim() == search);
                if (filter != null)
                {
                    var listPoke = new List<PokemonModel>();
                    listPoke.Add(filter);
                    ViewData["pl"] = listPoke;
                }
                else
                {
                    ViewData["pl"] = null;
                    ViewData["error"] = "no data found";
                }
            }
            else
            {
                var list = TempData["pokemanList"] as List<PokemonModel>;
                ViewData["pl"] = list;
            }
            return View();
            */
            //if a user choose the radio button option as Subject  
            string endpoint = "https://pokeapi.co/api/v2/pokemon?limit=100'";
            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var data1 = Response.Content.ReadAsStringAsync();
                        var listPK = new List<PokemonModel>();
                        var reuslt = data1.Result;
                        if (reuslt != null && reuslt.Length > 0)
                        {
                            var results = JsonConvert.DeserializeObject<RootObject>(reuslt);
                            foreach (var item in results.results)
                            {
                                var pokedata = new PokemonModel();
                                pokedata.Name = item.name;
                                string url = item.url;
                                url = url.Replace("https://pokeapi.co/api/v2/pokemon/", "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/");

                                pokedata.url = $"{url.Substring(0, url.Length - 1)}.png";
                                // $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{i}.png";
                                // i++;
                                listPK.Add(pokedata);
                                // pokedata.Name = item

                            }
                        }
                        var listPoke = new List<PokemonModel>();
                        // var pokemonListModel = JsonConvert.DeserializeObject<List<PokemonModel>>(await Response.Content.ReadAsStringAsync());
                        if (!string.IsNullOrEmpty(search))
                        {
                            var searchVal = listPK.Find(x => x.Name == search);
                            
                            listPoke.Add(searchVal);
                            TempData["pokemanList"] = listPoke;

                            TempData.Keep("pokemanList");
                            ViewData["pl"] = listPoke;
                        }
                        else
                        {
                            listPoke = listPK;
                        }
                        return View((listPoke.ToPagedList(page ?? 1, 5)));
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

        public async Task AddFavouriteAsync(string pokemonName)
        {
            var userDetails = Session["userSession"];
            if (userDetails != null)
            {
                var details = Session["userSession"] as LoginResponse;
                string endpoint = "https://localhost:7113/UserFavorite"+ "?"+"userId=" + details.Id + "&pokemonName=" + pokemonName;
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
                                if (checkExist == null)
                                {
                                    await AddFavAsync(details.Id, pokemonName);
                                    ViewBag.favourite = pokemonListModel.Count + 1;
                                }
                                
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

        
        private async Task AddFavAsync(int userId, string pokemonName)
        {
            UserFavourite user = new UserFavourite()
            {
                UserId = userId,
                PokemonName = pokemonName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            string endpoint = "https://localhost:7113/UserFavorite?userId=" + userId + "&pokemonName=" + pokemonName;
            using (HttpClient client = new HttpClient())
            {
                using (var Response = await client.PostAsync(endpoint,null))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                       
                    }
                   
                }
            }
        }

        private async Task RemoveFavAsync(int userId, string pokemonName)
        {
            UserFavourite user = new UserFavourite()
            {
                UserId = userId,
                PokemonName = pokemonName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            // UserFavorite?userId=7&pokemanId=1
            string endpoint = "https://localhost:7113/UserFavorite?userId="+userId + "&pokemonName=" + pokemonName;
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