// See https://aka.ms/new-console-template for more information
using DynamoDBExample;

Console.WriteLine("Hello, World!");
Book myBook = new Book
{
    Id = 1001,
    Title = "object persistence-AWS SDK for.NET SDK-Book 1001",
    ISBN = "111-1111111001",
    BookAuthors = new List<string> { "Author 1", "Author 2" },
};

DynamoDbOperation dbOperation = new DynamoDbOperation();
await dbOperation.AddBook(myBook);