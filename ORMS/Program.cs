using Microsoft.EntityFrameworkCore;
using ORMS;
using System.Security.Principal;

var toys = Utils.DeserializeFromFile<List<Toy>>("./Resources/Toys.json");
var dogs = Utils.DeserializeFromFile<List<Dog>>("./Resources/Dogs.json");
//var parks = Utils.DeserializeFromFile<List<Park>>("./Resources/Parks.json");
//var dogParkVisits = Utils.DeserializeFromFile<List<DogPark>>("./Resources/DogPark.json");

using(var db = new MyDbContext())
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    db.Dogs.AddRange(dogs);
    db.Toys.AddRange(toys);
    db.SaveChanges();

    var dogAndToys = db.Toys
                        .Include(d => d.Dog)
                        .ToList();

    foreach(var toy in dogAndToys)
    {
        Console.Write("Toy: "+ toy.Name);
        Console.Write("belongs to:" + toy.Dog.Name);
        Console.WriteLine();
    }
}