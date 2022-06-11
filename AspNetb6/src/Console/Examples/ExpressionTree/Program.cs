
using System.Linq.Expressions;
using System.Linq;

Expression<Func<int, int>> t = (x) => x * 10;
Func<int, int> f = t.Compile();

var x = f(3);
Console.WriteLine(x);

int[] something = new int[20];