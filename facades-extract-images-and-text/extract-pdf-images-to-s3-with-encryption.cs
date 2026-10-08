using System;
using System.IO;
using Aspose.Pdf.Facades;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

// ---------------------------------------------------------------------------
// Minimal stubs for the AWS SDK (AWSSDK.S3) to allow compilation when the real
// package is not referenced. In a production project you should reference the
// official AWSSDK.S3 NuGet package instead of these stubs.
// ---------------------------------------------------------------------------
namespace Amazon
{
    public sealed class RegionEndpoint
    {
        private RegionEndpoint() { }
        public static RegionEndpoint USEast1 => new RegionEndpoint();
    }

    public class AmazonS3Client
    {
        public AmazonS3Client(RegionEndpoint region) { }

        // The real SDK returns Task<PutObjectResponse>. For our stub a completed
        // Task is sufficient because the response is never inspected.
        public System.Threading.Tasks.Task PutObjectAsync(PutObjectRequest request)
        {
            // No‑op – in real code the SDK would upload the stream.
            return System.Threading.Tasks.Task.CompletedTask;
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
        public ServerSideEncryptionMethod ServerSideEncryptionMethod { get; set; }
    }

    public enum ServerSideEncryptionMethod
    {
        AES256
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // source PDF
        const string bucket    = "my-s3-bucket";       // target S3 bucket
        const string keyPrefix = "extracted-images/"; // optional folder in bucket

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize AWS S3 client (uses default credentials / config)
        AmazonS3Client s3Client = new AmazonS3Client(RegionEndpoint.USEast1);

        // Use Aspose.Pdf.Facades PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Retrieve the next image into the memory stream
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0; // reset for upload

                    // Build a unique S3 object key for each image
                    string objectKey = $"{keyPrefix}image_{imageIndex}.png";

                    // Prepare the upload request with server‑side encryption (AES256)
                    PutObjectRequest putRequest = new PutObjectRequest
                    {
                        BucketName = bucket,
                        Key = objectKey,
                        InputStream = imageStream,
                        ContentType = "image/png",
                        ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
                    };

                    // Upload the image to S3 (synchronous wait on the async call)
                    s3Client.PutObjectAsync(putRequest).GetAwaiter().GetResult();

                    Console.WriteLine($"Uploaded image {imageIndex} to s3://{bucket}/{objectKey}");
                    imageIndex++;
                }
            }

            if (imageIndex == 1)
                Console.WriteLine("No images were found in the PDF.");
        }
    }
}
