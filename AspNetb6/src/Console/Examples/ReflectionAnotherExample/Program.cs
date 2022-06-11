// See https://aka.ms/new-console-template for more information
using ReflectionAnotherExample;
using ReflectionAnotherExample.Cars;
using System.Reflection;
using System.Linq;

#region Interface
Console.WriteLine("Hello, World!");

ICar car1 = new Toyota();
IVehicle vehicle1 = new Toyota();
Toyota toyota = new Toyota();

toyota.SpeedUp(100);
car1.Start();
car1.SpeedUp(200);

vehicle1.SpeedUp(300);

Console.WriteLine(car1.CurrentSpeed);
Console.WriteLine(vehicle1.CurrentSpeed);

#endregion

#region Reflection

Assembly assembly = Assembly.GetExecutingAssembly();
Type[] types = assembly.GetTypes();

List<ICar?> cars = new List<ICar?>();

Console.WriteLine("Please select a car:");
foreach (Type type in types)
{
    if (type.GetInterfaces().Contains(typeof(ICar)))
    {
        ConstructorInfo? constructor = type.GetConstructor(new Type[] { });
        ICar? car = constructor?.Invoke(new object[] { }) as ICar;
        cars.Add(car);

        Console.WriteLine(car?.Model);
    }
}

var selectedCarModel = Console.ReadLine();
var selectedCar = cars.Where(x => x?.Model == selectedCarModel).First();

selectedCar?.Start();

while(true)
{
    Console.WriteLine("Select Your Option:");
    Console.WriteLine("1) SpeedUp");
    Console.WriteLine("2) SpeedDown");
    Console.WriteLine("3) Exit");
    var actionName = Console.ReadLine();

    if (actionName == "Exit")
        break;

    var carType = typeof(ICar);
    Console.WriteLine(carType);
    MethodInfo? actionMethod = carType?.GetMethod(actionName, BindingFlags.Public 
        | BindingFlags.Instance, new Type[] {typeof(double)});

    actionMethod?.Invoke(selectedCar, new object[] { 100 });
    Console.WriteLine(selectedCar?.CurrentSpeed);
}




#endregion

