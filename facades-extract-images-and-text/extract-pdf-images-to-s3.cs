using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;                     // PdfExtractor resides here
using System.Drawing.Imaging;                // ImageFormat (fully qualified when needed)

// ---------------------------------------------------------------------------
// Minimal stubs for the AWS SDK (Amazon.S3) when the real package is not
// referenced. These allow the sample to compile and run (the stub client does
// nothing) without pulling the full AWSSDK.S3 NuGet package. In a real project
// you should add the package reference instead of using these stubs.
// ---------------------------------------------------------------------------
namespace Amazon
{
    public class RegionEndpoint
    {
        public static readonly RegionEndpoint USEast1 = new RegionEndpoint("us-east-1");
        public string SystemName { get; }
        private RegionEndpoint(string systemName) => SystemName = systemName;
    }
}

namespace Amazon.S3
{
    using System.Threading;
    using System.Threading.Tasks;

    public class AmazonS3Client : IDisposable
    {
        private readonly Amazon.RegionEndpoint _region;
        public AmazonS3Client(Amazon.RegionEndpoint region) => _region = region;
        public void Dispose() { /* no resources to release in the stub */ }

        // The real SDK returns a PutObjectResponse; the stub returns a minimal one.
        public Task<PutObjectResponse> PutObjectAsync(PutObjectRequest request, CancellationToken cancellationToken = default)
        {
            // In a production scenario this would upload to S3. The stub simply
            // pretends the operation succeeded.
            return Task.FromResult(new PutObjectResponse { HttpStatusCode = System.Net.HttpStatusCode.OK });
        }
    }

    public class PutObjectRequest
    {
        public string BucketName { get; set; }
        public string Key { get; set; }
        public Stream InputStream { get; set; }
        public string ContentType { get; set; }
    }

    public class PutObjectResponse
    {
        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // source PDF
        const string bucketName = "my-s3-bucket";          // target S3 bucket
        // Choose the AWS region that matches your bucket
        var region = Amazon.RegionEndpoint.USEast1;

        // Initialize the S3 client (credentials are taken from the default AWS SDK chain)
        using (Amazon.S3.AmazonS3Client s3Client = new Amazon.S3.AmazonS3Client(region))
        {
            try
            {
                // Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);            // bind the PDF file
                    extractor.ExtractImage();              // prepare image extraction

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // Store the extracted image in a memory stream (default format is JPEG)
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            bool extracted = extractor.GetNextImage(imageStream);
                            if (!extracted)
                                break; // no more images or extraction failed

                            // Reset stream position before uploading
                            imageStream.Position = 0;

                            // Prepare the S3 upload request
                            var putRequest = new Amazon.S3.PutObjectRequest
                            {
                                BucketName = bucketName,
                                Key = $"image-{imageIndex}.jpg", // object key in S3
                                InputStream = imageStream,
                                ContentType = "image/jpeg"
                            };

                            // Upload synchronously (blocking) – adjust as needed for async usage
                            s3Client.PutObjectAsync(putRequest).GetAwaiter().GetResult();
                        }

                        imageIndex++;
                    }
                }

                Console.WriteLine("Image extraction and upload completed successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
