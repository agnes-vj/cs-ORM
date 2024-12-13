using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMS
{
    internal class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Loves { get; set; }
        public List<Toy> Toys { get; set; } = new List<Toy>();
        public List<DogParkVisits> FavouriteParks { get; set; } = new List<DogParkVisits> { };
    }
}
