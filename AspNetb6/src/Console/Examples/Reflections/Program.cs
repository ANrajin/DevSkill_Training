using System.Reflection;
namespace Reflections;
public class Program
{
    public static void Main(string[] args)
    {
        var product = new Product { Name = "Camera", Description = "Cannon", 
            Price = 30000, ProductID = 1 };

        var person = new Person { Name = "Jalaluddin", Age = 39, 
            Date = DateTime.Now, Id = 3 };

        Print(product);
        Print(person);
    }

    public static void Print(object item)
    {
        Console.WriteLine("---------------------------");
        Type type = item.GetType();
        Console.WriteLine($"Printing:{type.Name}");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");

        var properties = type.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            Console.WriteLine($"{property.Name} = {property.GetValue(item)}");
        }
        Console.WriteLine("---------------------------");
    }
}
