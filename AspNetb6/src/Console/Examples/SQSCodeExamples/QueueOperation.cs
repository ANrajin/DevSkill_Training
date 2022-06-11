using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using AWSSDK;

namespace SQSCodeExamples
{
    public class QueueOperation
    {
        private readonly AmazonSQSClient _client;
        public QueueOperation()
        {
            _client = new AmazonSQSClient();
        }

        public async Task<string> CreateQueueAsync(string queueName)
        {
            CreateQueueRequest request = new CreateQueueRequest(queueName);
            CreateQueueResponse response = await _client.CreateQueueAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return response.QueueUrl;
            else
                return null;
        }

        public async Task<string> AddMessage(string url, string body)
        {
            SendMessageRequest request = new SendMessageRequest(url, body);
            var response = await _client.SendMessageAsync(request);
            
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return response.MessageId;
            else
                return null;
        }
    }
}
