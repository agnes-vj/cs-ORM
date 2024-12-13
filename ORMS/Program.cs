using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ORMS;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var toys = Utils.DeserializeFromFile<List<Toy>>("./Resources/Toys.json");
var dogs = Utils.DeserializeFromFile<List<Dog>>("./Resources/Dogs.json");
var parks = Utils.DeserializeFromFile<List<Park>>("./Resources/Parks.json");
var dogParkVisits = Utils.DeserializeFromFile<List<DogParkVisits>>("./Resources/DogPark.json");

using (var db1 = new MyDbContext())
{

    //Get All Owners

    var owners = db1.Owners.ToList();
    foreach (var owner in owners)
    {
        Console.WriteLine($"{owner.FirstName} {owner.LastName}");
    }
}
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

    //Query the Parks table and see a list of dogs visited.


    var parksWithDogs = db.Parks
                           .Include(pd => pd.DogsVisited)
                           .ToList();
    foreach (var park in parksWithDogs)
    {
        Console.Write($"Park Id: {park.Id}| Park Name: {park.Name}| Rating: {park.RatingOutOf10} | Dogs Visited: ");
        foreach (var dogParkVisit in park.DogsVisited)
        {
            Console.Write(dogParkVisit.Dog.Name + ", ");
        }
        Console.WriteLine();
    }

    // Query the Dogs table and see a list of their favourite parks they have been to.
    Console.WriteLine("Dogs with visited Parks");
    var dogWithParks = db.Dogs
                          .Include(pd => pd.FavouriteParks)
                          .ToList();
    foreach (var dog in dogWithParks)
    {
        Console.Write($"Dog Id: {dog.Id}| Dog Name: {dog.Name}| Breed : {dog.Breed} | Parks Visited: ");
        foreach (var dogParkVisit in dog.FavouriteParks)
        {
            Console.Write(dogParkVisit.Park.Name + ", ");
        }
        Console.WriteLine();
    }

    //Get All Owners
    Console.WriteLine("Owners From Inside");
    var owners = db.Owners.ToList();
    foreach (var owner in owners)
    {
        Console.WriteLine($"{owner.FirstName} {owner.LastName}");
    }
}
Console.WriteLine("Outside at the End");
using (var db1 = new MyDbContext())
{

    //Get All Owners

    var owners = db1.Owners.ToList();
    foreach (var owner in owners)
    {
        Console.WriteLine($"{owner.FirstName} {owner.LastName}");
    }
}
