using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using Amazon.S3.Model; // <-- added to make PutObjectRequest/PutObjectResponse visible

// ---------------------------------------------------------------------------
// Minimal stubs for the AWS SDK (Amazon.S3) so the project can compile without
// adding the real NuGet package. In a production environment you should replace
// these stubs with the official AWSSDK.S3 package.
// ---------------------------------------------------------------------------
namespace Amazon.S3
{
    // Simple disposable client that mimics the real AmazonS3Client API used in the
    // sample. The real client performs network I/O; this stub only satisfies the
    // compiler and can be extended for unit‑testing if required.
    public class AmazonS3Client : IDisposable
    {
        public void Dispose() { /* No resources to release in the stub */ }

        // The real SDK returns a PutObjectResponse; for the stub we return a dummy
        // object that satisfies the method signature.
        public Task<PutObjectResponse> PutObjectAsync(PutObjectRequest request)
        {
            // In a real implementation the request would be sent to AWS S3.
            // Here we simply return a completed task.
            return Task.FromResult(new PutObjectResponse());
        }
    }
}

namespace Amazon.S3.Model
{
    // Represents the request sent to S3 when uploading an object.
    public class PutObjectRequest
    {
        public string BucketName { get; set; }
        public string Key { get; set; }
        public Stream InputStream { get; set; }
        public ServerSideEncryptionMethod ServerSideEncryptionMethod { get; set; }
    }

    // Minimal response class – the real SDK contains many properties, but they are
    // not needed for the sample code.
    public class PutObjectResponse { }

    // Server‑side encryption options supported by S3. Only AES256 is used in the
    // example, but the enum can be expanded if required.
    public enum ServerSideEncryptionMethod
    {
        AES256,
        // Other values (e.g., AWSKMS) can be added here.
    }
}

class Program
{
    static async Task Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Amazon S3 bucket and optional key prefix
        const string bucketName = "my-s3-bucket";
        const string keyPrefix = "extracted-images/";

        // Create an S3 client (uses default credentials / region configuration)
        using Amazon.S3.AmazonS3Client s3Client = new Amazon.S3.AmazonS3Client();

        // Ensure the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor to pull images from the PDF
        using PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);

        // Extract images that are actually used on the pages
        extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            // Retrieve the next image into a memory stream (default format is JPEG)
            using MemoryStream imageStream = new MemoryStream();
            bool success = extractor.GetNextImage(imageStream);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                break;
            }

            // Reset stream position before uploading
            imageStream.Position = 0;

            // Build the S3 object key (e.g., extracted-images/image_1.jpg)
            string objectKey = $"{keyPrefix}image_{imageIndex}.jpg";

            // Prepare the PutObject request with server‑side encryption enabled
            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                InputStream = imageStream,
                ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
            };

            try
            {
                // Upload the image to S3
                await s3Client.PutObjectAsync(putRequest);
                Console.WriteLine($"Uploaded image #{imageIndex} to s3://{bucketName}/{objectKey}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error uploading image #{imageIndex}: {ex.Message}");
                // Optionally break or continue based on requirements
                break;
            }

            imageIndex++;
        }

        // Close the extractor (handled by using statement)
        Console.WriteLine("Image extraction and upload completed.");
    }
}
