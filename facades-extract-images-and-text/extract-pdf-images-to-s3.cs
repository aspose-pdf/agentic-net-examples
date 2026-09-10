using System;
using System.IO;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Stub implementations for AWS SDK types (Amazon.S3) when the real package is
// not referenced. In a real project you should add the NuGet package
// "AWSSDK.S3" and remove these stubs.
// ---------------------------------------------------------------------------
namespace Amazon
{
    public class RegionEndpoint
    {
        public static readonly RegionEndpoint USEast1 = new RegionEndpoint();
    }
}

namespace Amazon.S3
{
    public class AmazonS3Client
    {
        public AmazonS3Client(Amazon.RegionEndpoint region) { }
        public Task PutObjectAsync(Amazon.S3.Model.PutObjectRequest request)
        {
            // Simple stub – in production this uploads to S3.
            Console.WriteLine($"[Stub] Uploading '{request.FilePath}' to bucket '{request.BucketName}' as key '{request.Key}'.");
            return Task.CompletedTask;
        }
    }
}

namespace Amazon.S3.Model
{
    public class PutObjectRequest
    {
        // Made nullable to satisfy the compiler warnings for non‑nullable properties.
        public string? BucketName { get; set; }
        public string? Key { get; set; }
        public string? FilePath { get; set; }
        public string? ContentType { get; set; }
    }
}

class Program
{
    static async Task Main()
    {
        const string pdfPath = "input.pdf";               // Path to source PDF
        const string bucketName = "my-bucket";            // S3 bucket name
        const string s3Folder = "pdf-images/";            // Optional folder inside bucket

        // Ensure a PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(pdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(pdfPath);
        }

        // Initialize S3 client (adjust region as needed)
        var s3Client = new Amazon.S3.AmazonS3Client(Amazon.RegionEndpoint.USEast1);

        // Use PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);        // Bind the PDF file
            extractor.ExtractImage();          // Prepare for image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Create a temporary file for the extracted image
                string tempFile = Path.GetTempFileName();

                // Save the next image as PNG (you can choose other formats)
#pragma warning disable CA1416 // ImageFormat.Png is platform‑specific, but acceptable for this demo
                extractor.GetNextImage(tempFile, ImageFormat.Png);
#pragma warning restore CA1416

                // Build the S3 object key (e.g., pdf-images/image-1.png)
                string s3Key = $"{s3Folder}image-{imageIndex}.png";

                // Prepare the upload request
                var putRequest = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3Key,
                    FilePath = tempFile,
                    ContentType = "image/png"
                };

                // Upload the image to S3
                await s3Client.PutObjectAsync(putRequest);

                // Clean up the temporary file
                File.Delete(tempFile);

                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted and uploaded to S3.");
    }
}
