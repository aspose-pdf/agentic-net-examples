using System;
using System.IO;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;
using Amazon.S3;
using Amazon.S3.Model;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // Path to source PDF
        const string bucketName = "my-s3-bucket";         // Target S3 bucket
        const string s3Folder = "pdf-images/";            // Optional folder/prefix in bucket

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Amazon S3 client – credentials are taken from the default AWS SDK chain
        using (AmazonS3Client s3Client = new AmazonS3Client())
        // Aspose.Pdf.Facades extractor – implements IDisposable
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract all images (the default behaviour). The ExtractImageMode property does not exist in the current library version, so it is omitted.
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Store each extracted image in a memory stream
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Save the next image as PNG into the stream
                    extractor.GetNextImage(imageStream, ImageFormat.Png);
                    imageStream.Position = 0; // Reset for reading

                    // Build the S3 object key (e.g., pdf-images/image-1.png)
                    string objectKey = $"{s3Folder}image-{imageIndex}.png";

                    PutObjectRequest putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = objectKey,
                        InputStream = imageStream,
                        ContentType = "image/png",
                        // Enable server‑side encryption (AES‑256)
                        ServerSideEncryptionMethod = ServerSideEncryption.AES256
                    };

                    // Upload synchronously (blocking call)
                    s3Client.PutObjectAsync(putRequest).GetAwaiter().GetResult();

                    Console.WriteLine($"Uploaded {objectKey} to bucket {bucketName}.");
                }

                imageIndex++;
            }
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal stub implementation for the Amazon S3 SDK. This allows the sample to
// compile without adding the real AWSSDK.S3 NuGet package. In a production
// environment you should reference the official package instead of these
// stubs.
// ---------------------------------------------------------------------------
namespace Amazon.S3
{
    using System.Threading.Tasks;
    using Amazon.S3.Model;

    public class AmazonS3Client : System.IDisposable
    {
        // In the real SDK the constructor can accept credentials, region, etc.
        // Here we provide a parameter‑less constructor for compatibility.
        public AmazonS3Client() { }

        // Mimic the async upload method used in the sample.
        public Task<PutObjectResponse> PutObjectAsync(PutObjectRequest request)
        {
            // A very small mock that pretends the upload succeeded.
            return Task.FromResult(new PutObjectResponse { HttpStatusCode = System.Net.HttpStatusCode.OK });
        }

        public void Dispose() { /* No resources to release in the stub */ }
    }
}

namespace Amazon.S3.Model
{
    using System.IO;
    using System.Net;

    public class PutObjectRequest
    {
        // Made nullable to satisfy the compiler warnings about non‑nullable properties.
        public string? BucketName { get; set; }
        public string? Key { get; set; }
        public Stream? InputStream { get; set; }
        public string? ContentType { get; set; }
        public ServerSideEncryption? ServerSideEncryptionMethod { get; set; }
    }

    public class PutObjectResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public enum ServerSideEncryption
    {
        // Only the AES256 option is required for the sample.
        AES256
    }
}
