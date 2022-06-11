using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using AWSSDK;

namespace S3BucketExample
{
    public class S3BucketOperations
    {
        private readonly AmazonS3Client _client;
        public S3BucketOperations()
        {
            _client = new AmazonS3Client();
        }

        public async Task UplodFileAsync(string bucketName, string fileName, string filePath)
        {
            // 2. Put the object-set ContentType and add metadata.
            var putRequest2 = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
                FilePath = filePath,
                ContentType = "text/plain"
            };

            putRequest2.Metadata.Add("x-amz-meta-title", "someTitle");
            PutObjectResponse response2 = await _client.PutObjectAsync(putRequest2);
        }
    }
}
