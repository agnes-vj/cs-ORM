using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ORMS;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var toys = Utils.DeserializeFromFile<List<Toy>>("./Resources/Toys.json");
var dogs = Utils.DeserializeFromFile<List<Dog>>("./Resources/Dogs.json");
var parks = Utils.DeserializeFromFile<List<Park>>("./Resources/Parks.json");
var dogParkVisits = Utils.DeserializeFromFile<List<DogParkVisits>>("./Resources/DogPark.json");

//using (var db1 = new MyDbContext())
//{

//    //Get All Owners

//    var owners = db1.Owners.ToList();
//    foreach (var owner in owners)
//    {
//        Console.WriteLine($"{owner.FirstName} {owner.LastName}");
//    }
//}
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

    ////Get All Owners
    //Console.WriteLine("Owners From Inside");
    //var owners = db.Owners.ToList();
    //foreach (var owner in owners)
    //{
    //    Console.WriteLine($"{owner.FirstName} {owner.LastName}");
    //}

    //Get a list of dogs that have visited "Alexandra Park".
    Console.WriteLine("Dogs that visited Alexandra Park:");
    var dogsPark = db.Parks.Include(p=>p.DogsVisited).ToList();
    var dogVisitedAlexPark = dogsPark.Where(d => d.Name == "Alexandra Park").ToList();

    foreach(var park in dogVisitedAlexPark)
    {
        Console.WriteLine("Park Name: " + park.Name + "| Dogs Visited: ");
        foreach(var dogAlexPark in park.DogsVisited)
        {
            Console.Write(dogAlexPark.Dog.Name + ", ");
        }
    }

    //Get a list of all the toys owned by all the dogs that have visited "Heaton Park".
    Console.WriteLine("Toys that belong to dogs who visited Heaton Park: ");
    var dogsPark2 = db.Parks.Include(p => p.DogsVisited).ToList();
    dogsPark2
        .Where(d => d.Name == "Heaton Park")
        .ToList()
        .ForEach(p => p.DogsVisited
                                .ForEach(d => d.Dog.Toys
                                                    .ForEach(t => Console.Write(t.Name + " "))));

    /*
    foreach (var park in dogVisitedHeatonPark)
    {
        Console.WriteLine("Park Name: " + park.Name + "| All Toys owned by dogs who visited Heaton Park: ");
        foreach (var dogHeatonPark in park.DogsVisited)
        {
            foreach(var toy in dogHeatonPark.Dog.Toys)
            {
                Console.Write(toy.Name, " ");
            }
        }
    }*/

    //Get the park that has had the most visitors.
    Console.WriteLine("park that has had the most visitors");

    Dictionary<string, int> ParkVisitors = new Dictionary<string, int>();
    db.Parks
        .Include(p => p.DogsVisited)
        .ToList()
        .ForEach(p => ParkVisitors.Add(p.Name, p.DogsVisited.Count));

    var parkVisitedMost = ParkVisitors.OrderByDescending(p => p.Value).First();
    Console.WriteLine(parkVisitedMost.Key + "was visited " + parkVisitedMost.Value + " Times");

}
//Console.WriteLine("Outside at the End");
//using (var db1 = new MyDbContext())
//{

//    //Get All Owners

//    var owners = db1.Owners.ToList();
//    foreach (var owner in owners)
//    {
//        Console.WriteLine($"{owner.FirstName} {owner.LastName}");
//    }
//}
