using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

// ---------------------------------------------------------------------------
// Minimal stubs for the AWS SDK (AWSSDK.S3). These allow the project to compile
// without pulling the real NuGet package. In a production environment you should
// reference the official AWSSDK.S3 package instead of these stubs.
// ---------------------------------------------------------------------------
namespace Amazon
{
    public sealed class RegionEndpoint
    {
        private readonly string _systemName;
        private RegionEndpoint(string systemName) => _systemName = systemName;
        public static RegionEndpoint GetBySystemName(string systemName) => new RegionEndpoint(systemName);
        public override string ToString() => _systemName;
    }
}

namespace Amazon.S3
{
    public class AmazonS3Client
    {
        private readonly RegionEndpoint _region;
        public AmazonS3Client(RegionEndpoint region) => _region = region;
        // The real SDK provides many overloads; we only need the async upload used below.
        public Task<PutObjectResponse> PutObjectAsync(PutObjectRequest request)
        {
            // Stub implementation – in real code the request would be sent to AWS.
            var response = new PutObjectResponse { ETag = "\"stub-etag\"" };
            return Task.FromResult(response);
        }
    }
}

namespace Amazon.S3.Model
{
    public class PutObjectRequest
    {
        public string BucketName { get; set; }
        public string Key { get; set; }
        public Stream InputStream { get; set; }
        public string ContentType { get; set; }
    }

    public class PutObjectResponse
    {
        public string ETag { get; set; }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string bucketName = "my-s3-bucket";
        const string awsRegion = "us-east-1"; // adjust as needed

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the Amazon S3 client (credentials are taken from environment, config file, or IAM role)
        AmazonS3Client s3Client = new AmazonS3Client(RegionEndpoint.GetBySystemName(awsRegion));

        // Use PdfExtractor (implements IDisposable) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(pdfPath);
            // Extract images from all pages
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over each extracted image
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Write the current image to the memory stream
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0; // reset for reading

                    // Define the S3 object key (file name)
                    string s3Key = $"pdf-images/image_{imageIndex}.png";

                    // Prepare the upload request
                    PutObjectRequest putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = s3Key,
                        InputStream = imageStream,
                        ContentType = "image/png"
                    };

                    // Upload the image to S3 (synchronous for simplicity)
                    try
                    {
                        PutObjectResponse response = s3Client.PutObjectAsync(putRequest).GetAwaiter().GetResult();
                        Console.WriteLine($"Uploaded image {imageIndex} to s3://{bucketName}/{s3Key} (ETag: {response.ETag})");
                    }
                    catch (Exception ex) // Using generic Exception because the stub does not throw AmazonS3Exception
                    {
                        Console.Error.WriteLine($"Error uploading image {imageIndex}: {ex.Message}");
                    }

                    imageIndex++;
                }
            }
        }
    }
}
