using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for AWS SDK types when the real SDK is not referenced.
// These allow the project to compile and run (the upload will be a no‑op).
// If the AWS SDK NuGet package is added, the real types will be used instead.
// ---------------------------------------------------------------------------
#if !AWS_SDK_PRESENT
namespace Amazon
{
    public class RegionEndpoint
    {
        public static RegionEndpoint GetBySystemName(string name) => new RegionEndpoint();
    }
}

namespace Amazon.S3
{
    using System.Threading.Tasks;

    public class AmazonS3Client
    {
        public AmazonS3Client(Amazon.RegionEndpoint endpoint) { }
        public Task PutObjectAsync(PutObjectRequest request) => Task.CompletedTask;
    }

    public class PutObjectRequest
    {
        public string BucketName { get; set; }
        public string Key { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
    }
}
#endif

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument or default)
        string pdfPath = args.Length > 0 ? args[0] : "input.pdf";

        // Target S3 bucket name (second argument or default)
        string bucketName = args.Length > 1 ? args[1] : "my-bucket";

        // AWS region (third argument or default)
        string regionName = args.Length > 2 ? args[2] : "us-east-1";

        // Initialise S3 client – works with real SDK or the stub above.
        var s3Client = new Amazon.S3.AmazonS3Client(Amazon.RegionEndpoint.GetBySystemName(regionName));

        // Ensure the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Extract images using Aspose.Pdf.Facades.PdfExtractor
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage(); // Prepare extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Create a temporary file for the extracted image
                string tempFile = Path.GetTempFileName();

                // Save the next image as PNG (you can choose other formats)
                extractor.GetNextImage(tempFile, ImageFormat.Png);

                // Build S3 object key (path within the bucket)
                string key = $"images/{Path.GetFileNameWithoutExtension(pdfPath)}_image_{imageIndex}.png";

                // Prepare the upload request
                var putRequest = new Amazon.S3.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    FilePath = tempFile,
                    ContentType = "image/png"
                };

                // Upload synchronously (blocking)
                try
                {
                    s3Client.PutObjectAsync(putRequest).GetAwaiter().GetResult();
                    Console.WriteLine($"Uploaded image {imageIndex} to s3://{bucketName}/{key}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to upload image {imageIndex}: {ex.Message}");
                }
                finally
                {
                    // Clean up the temporary file
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and upload completed.");
    }
}
