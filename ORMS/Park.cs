using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMS
{
    internal class Park
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1,10)]
        public int RatingOutOf10 { get; set; }

        public List<DogParkVisits> DogsVisited { get; set; } = new List<DogParkVisits> { };
    }
}
