using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ORMS;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var toys = Utils.DeserializeFromFile<List<Toy>>("./Resources/Toys.json");
var dogs = Utils.DeserializeFromFile<List<Dog>>("./Resources/Dogs.json");
var parks = Utils.DeserializeFromFile<List<Park>>("./Resources/Parks.json");
var dogParkVisits = Utils.DeserializeFromFile<List<DogParkVisits>>("./Resources/DogPark.json");

using (var db = new MyDbContext())
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    db.Dogs.AddRange(dogs);
    db.Toys.AddRange(toys);
    db.SaveChanges();

    var dogAndToys = db.Toys
                        .Include(d => d.Dog)
                        .ToList();

    foreach (var toy in dogAndToys)
    {
        Console.Write("Toy: " + toy.Name);
        Console.Write("belongs to:" + toy.Dog.Name);
        Console.WriteLine();
    }

    var dogToys = db.Dogs
                    .Include(dt => dt.Toys)
                    .ToList();
    foreach (var dogtoy in dogToys)
    {
        Console.Write(dogtoy.Name + " has Toys : ");
        foreach (var toy in dogtoy.Toys)
        {
            Console.Write(toy.Name + ", ");
        }
        Console.WriteLine();
    }

    //Add a new Dog
    Dog newDog = new()
    {
        Name = "Ginger1",
        Breed = "Pomeranian",
        Loves = "Play"
    };
    db.Dogs.Add(newDog);
    db.SaveChanges();
    //Add a new Toy

    Toy newToy = new()
    {
        Name = "NewRubberDucky",
        Squeaks = true
    };

    //update newToy with newDogId
    db.Toys.Add(newToy);
    newToy.DogId = newDog.Id;
    db.SaveChanges();

    //add parks
    db.Parks.AddRange(parks);
    db.SaveChanges();

    //add dogparkvisits
    db.DogParkVisits.AddRange(dogParkVisits);
    db.SaveChanges();

    //Query the Dogs table and see a list of their favourite parks they have been to.
    var dogFavouriteParks = db.DogParkVisits
                                .Include(d => d.Dog)
                                .Include(d => d.Park)
                                .ToList();
    foreach (var dogFavPark in dogFavouriteParks)
    {
        Console.WriteLine($"Park Id: {dogFavPark.ParkId}| Park Name: {dogFavPark.Park.Name}| Rating: {dogFavPark.Park.RatingOutOf10} | Dogs Visited: "  );
        
    }
}
    