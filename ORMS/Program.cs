using ORMS;

var toys = Utils.DeserializeFromFile<List<Toy>>("./Resources/Toys.json");
var dogs = Utils.DeserializeFromFile<List<Dog>>("./Resources/Dogs.json");
//var parks = Utils.DeserializeFromFile<List<Park>>("./Resources/Parks.json");
//var dogsParks = Utils.DeserializeFromFile<List<DogPark>>("./Resources/DogPark.json");