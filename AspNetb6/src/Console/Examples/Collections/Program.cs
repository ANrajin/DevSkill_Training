using Collections;
using System.Collections;
using System.Collections.Specialized;

List<int> ages = new();
Dictionary<string, double> grades = new Dictionary<string, double>();
grades.Add("jalaluddin", 3.4);
var mygrade = grades["jalaluddin"];
grades["jalaluddin"] = 3.5;

HashSet<double> numbers = new();
numbers.Add(3.3);

Stack<int> dices = new Stack<int>();
Queue<double> amounts = new Queue<double>();
SortedList<int, int> points = new SortedList<int, int>();
SortedDictionary<int, int> words = new SortedDictionary<int, int>();
SortedSet<int> scores = new SortedSet<int>();
LinkedList<int> linkedlist = new LinkedList<int>();

ArrayList items = new ArrayList();
Stack stack = new Stack();
Queue queue = new Queue();
Hashtable hash = new Hashtable();
SortedList list = new SortedList();
NameValueCollection pairs = new NameValueCollection();

People people = new People(10);
people.Add(new Person() { Name = "jalaluddin" });
people.Add(new Person() { Name = "tareq" });
foreach (var person in people)
{
    if(person != null)
        Console.WriteLine(person.Name);
    else
        Console.WriteLine("No Item.");
}
