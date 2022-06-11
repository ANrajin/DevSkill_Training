// See https://aka.ms/new-console-template for more information
using SQSCodeExamples;

Console.WriteLine("Hello, World!");


QueueOperation queueOperation = new QueueOperation();
/*
string url = await queueOperation.CreateQueueAsync("DemoQueue");
Console.WriteLine(url);
*/
var url = "https://sqs.us-east-1.amazonaws.com/847888492411/DemoQueue";
var body = "hello world";
var body2 = "{ value = 'test'}";
var messageId = await queueOperation.AddMessage(url, body);
var messageId2 = await queueOperation.AddMessage(url, body2);

Console.WriteLine(messageId);
Console.WriteLine(messageId2);
