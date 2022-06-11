using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDBExample
{
    public class DynamoDbOperation
    {
        private static AmazonDynamoDBClient _client;
        private DynamoDBContext _context;
        public DynamoDbOperation()
        {
            _client = new AmazonDynamoDBClient();
            _context = new DynamoDBContext(_client);

        }

        public async Task AddBook(Book book)
        {
            

            // Save the book.
            await _context.SaveAsync(book);
        }
    }
}
