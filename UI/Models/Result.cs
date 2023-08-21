using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{
    public class Result
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Pokemon
    {
        public int count { get; set; }
        public object previous { get; set; }
        public List<Result> results { get; set; }
    }

    public class RootObject
    {
        public int count { get; set; }
        public object previous { get; set; }
        public List<Result> results { get; set; }
    }

}