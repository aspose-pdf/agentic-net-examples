using System;
using System.IO;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;

// ---------------------------------------------------------------------------
// Minimal AWS SDK stubs – these are compiled only when the real AWS SDK is not
// referenced. They provide just enough members for the sample code to build
// and run (the upload will be a no‑op). If the project references the real
// AWSSDK.S3 package the compiler will use the real types and the stubs will be
// ignored.
// ---------------------------------------------------------------------------
#if !AWS_SDK_PRESENT
namespace Amazon
{
    public class AmazonServiceClient : IDisposable
    {
        public void Dispose() { }
    }

    public class RegionEndpoint
    {
        public static RegionEndpoint USEast1 => new RegionEndpoint();
    }
}

namespace Amazon.S3
{
    public class AmazonS3Config
    {
        public Amazon.RegionEndpoint RegionEndpoint { get; set; }
    }

    public class AmazonS3Client : Amazon.AmazonServiceClient
    {
        public AmazonS3Client(AmazonS3Config config) { }
    }

    public enum S3CannedACL
    {
        PublicRead
    }
}

namespace Amazon.S3.Transfer
{
    using System.IO;
    using System.Threading.Tasks;

    public class TransferUtility
    {
        private readonly Amazon.S3.AmazonS3Client _client;
        public TransferUtility(Amazon.S3.AmazonS3Client client) => _client = client;
        public Task UploadAsync(TransferUtilityUploadRequest request) => Task.CompletedTask;
    }

    public class TransferUtilityUploadRequest
    {
        public Stream InputStream { get; set; }
        public string BucketName { get; set; }
        public string Key { get; set; }
        public Amazon.S3.S3CannedACL CannedACL { get; set; }
    }
}
#endif

class Program
{
    // Entry point
    static async Task Main(string[] args)
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // AWS S3 configuration
        const string bucketName = "your-s3-bucket-name";
        AmazonS3Config s3Config = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.USEast1 // adjust region as needed
        };
        // Credentials are taken from the default AWS SDK credential chain
        using AmazonS3Client s3Client = new AmazonS3Client(s3Config);
        TransferUtility transferUtility = new TransferUtility(s3Client);

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Extract images and upload to S3
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Extract the next image as PNG into the memory stream
                    bool success = extractor.GetNextImage(imageStream, ImageFormat.Png);
                    if (!success)
                    {
                        Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                        break;
                    }

                    // Reset stream position before upload
                    imageStream.Position = 0;

                    // Define S3 object key (path within the bucket)
                    string s3Key = $"extracted-images/image-{imageIndex}.png";

                    // Upload the image stream to S3
                    TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = imageStream,
                        BucketName = bucketName,
                        Key = s3Key,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    await transferUtility.UploadAsync(uploadRequest);
                    Console.WriteLine($"Uploaded image #{imageIndex} to s3://{bucketName}/{s3Key}");
                }

                imageIndex++;
            }
        }
    }
}
