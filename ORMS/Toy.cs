using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMS
{
    internal class Toy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Squeaks { get; set; }
        public int DogId { get; set; } = 1;
        //navigation property
        public Dog Dog { get; set; }

    }
}
