// See https://aka.ms/new-console-template for more information
using CreationalPatterns;
using System.Text;

Zoombie zoombie = new Zoombie();
zoombie.Strength = 200;
zoombie.Speed = 100;
zoombie.Health = 500;
zoombie.Look = "Red";

var zoombie2 = zoombie.Copy();


CarBuilder builder = new CarBuilder();
Car c = builder.AddEngine()
                .AddColor("red")
                .AddWheels()
                .Build();


StringBuilder stringBuilder = new StringBuilder();
stringBuilder.Append("hello").Append("word").AppendLine();
string message = stringBuilder.ToString();

Car c1 = CarFactory.CreateCar(Presets.Preset1);

MilitaryFactory militaryFactory = new RussianMilitaryFactory();

Fighter f = militaryFactory.GetFighter();
Ship s = militaryFactory.GetShip();