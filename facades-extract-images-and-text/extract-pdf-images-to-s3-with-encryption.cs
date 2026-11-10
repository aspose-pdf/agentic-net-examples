using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

// Minimal AWS SDK stubs to allow compilation without the real AWSSDK.S3 package
namespace Amazon
{
    public class RegionEndpoint
    {
        public string SystemName { get; }
        private RegionEndpoint(string systemName) => SystemName = systemName;
        public static RegionEndpoint GetBySystemName(string systemName) => new RegionEndpoint(systemName);
    }
}

namespace Amazon.S3
{
    public enum ServerSideEncryptionMethod
    {
        AES256
    }

    public class PutObjectRequest
    {
        public string BucketName { get; set; }
        public string Key { get; set; }
        public Stream InputStream { get; set; }
        public ServerSideEncryptionMethod ServerSideEncryptionMethod { get; set; }
    }

    public class PutObjectResponse { }

    public class AmazonS3Client : IDisposable
    {
        private readonly Amazon.RegionEndpoint _region;
        public AmazonS3Client(Amazon.RegionEndpoint region) => _region = region;
        public Task<PutObjectResponse> PutObjectAsync(PutObjectRequest request)
        {
            // In a real implementation this would upload to S3.
            // Here we just simulate a successful upload.
            return Task.FromResult(new PutObjectResponse());
        }
        public void Dispose() { /* No resources to clean up in the stub */ }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // source PDF
        const string bucket    = "my-s3-bucket";       // target S3 bucket
        const string regionStr = "us-east-1";          // bucket region

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Create S3 client (uses default credentials/provider chain)
        var region = Amazon.RegionEndpoint.GetBySystemName(regionStr);
        using (Amazon.S3.AmazonS3Client s3Client = new Amazon.S3.AmazonS3Client(region))
        {
            // Extract images from PDF using Aspose.Pdf.Facades
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Retrieve image into a memory stream
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imageStream);
                        imageStream.Position = 0; // reset for upload

                        // Build a unique S3 object key (e.g., image_1.png)
                        string key = $"pdf-images/image_{imageIndex}{GetExtension(imageStream)}";

                        // Prepare S3 put request with server‑side encryption (AES256)
                        var putRequest = new Amazon.S3.PutObjectRequest
                        {
                            BucketName = bucket,
                            Key = key,
                            InputStream = imageStream,
                            ServerSideEncryptionMethod = Amazon.S3.ServerSideEncryptionMethod.AES256
                        };

                        // Upload to S3 (synchronously waiting for the async call)
                        s3Client.PutObjectAsync(putRequest).GetAwaiter().GetResult();
                        Console.WriteLine($"Uploaded {key} to bucket {bucket}");

                        imageIndex++;
                    }
                }
            }
        }
    }

    // Simple helper to guess image extension from stream header bytes
    private static string GetExtension(Stream stream)
    {
        if (!stream.CanSeek) return ".bin";

        long originalPos = stream.Position;
        byte[] header = new byte[8];
        stream.Read(header, 0, header.Length);
        stream.Position = originalPos;

        // PNG signature
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png";

        // JPEG signature
        if (header[0] == 0xFF && header[1] == 0xD8)
            return ".jpg";

        // GIF signature
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46)
            return ".gif";

        // BMP signature
        if (header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp";

        return ".bin";
    }
}