// See https://aka.ms/new-console-template for more information
using S3BucketExample;

S3BucketOperations bucketOperations = new S3BucketOperations();
await bucketOperations.UplodFileAsync("devskilldemo", "TextFile1.txt", @"C:\Training\aspnet-b6\src\Console\Examples\S3BucketExample\TextFile1.txt");
