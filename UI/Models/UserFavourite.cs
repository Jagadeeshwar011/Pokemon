﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{
    public class UserFavourite
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }

        public string PokemonName { get; set; }

    }
}