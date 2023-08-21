using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{
    public class DetailsModel
    {
        public List<Instance> Abilities { get; set; }
    }

    public class Ability
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Instance
    {
        public Ability Ability { get; set; }
    }

    

}