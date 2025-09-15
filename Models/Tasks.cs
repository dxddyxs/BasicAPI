using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAPI.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }

        // construct no params
        public Tasks()
        {
            Name = "";
            Description = "";
            Done = false;
        }

        //construct with params
        public Tasks(string name, string description)
        {
            Name = name;
            Description = description;
            Done = false;
        }
    }
}