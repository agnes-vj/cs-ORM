using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMS
{
    internal class DogParkVisits
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public int ParkId { get; set; }

        public Dog Dog { get; set; }
        public Park Park { get; set; }
    }
}
