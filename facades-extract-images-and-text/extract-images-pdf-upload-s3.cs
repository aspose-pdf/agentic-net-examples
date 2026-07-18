using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

class Program
{
    static void Main()
    {
        // -------------------------------------------------------------------
        // 1. Create a sample PDF that contains at least one image.
        //    This makes the example self‑contained for the sandbox.
        // -------------------------------------------------------------------
        const string pdfPath = "input.pdf";
        CreateSamplePdfWithImage(pdfPath);

        // -------------------------------------------------------------------
        // 2. Amazon S3 configuration (uses default credential chain).
        // -------------------------------------------------------------------
        const string bucketName = "my-s3-bucket";
        const string s3Folder = "extracted-images/"; // ensure it ends with '/' if needed.
        AmazonS3Client s3Client = new AmazonS3Client(RegionEndpoint.USEast1);

        // -------------------------------------------------------------------
        // 3. Extract images from the PDF and upload each to S3.
        // -------------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // ImageFormat.Png is Windows‑only; suppress the platform warning.
#pragma warning disable CA1416 // Validate platform compatibility
                    extractor.GetNextImage(imageStream, ImageFormat.Png);
#pragma warning restore CA1416 // Validate platform compatibility
                    imageStream.Position = 0;

                    string objectKey = $"{s3Folder}image-{imageIndex}.png";
                    var putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = objectKey,
                        InputStream = imageStream,
                        ContentType = "image/png"
                    };

                    // Synchronous upload (blocking).
                    s3Client.PutObjectAsync(putRequest).GetAwaiter().GetResult();
                }
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and upload completed.");
    }

    private static void CreateSamplePdfWithImage(string outputPath)
    {
        // Create a simple bitmap (red square) in memory.
        using (Bitmap bmp = new Bitmap(100, 100))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color.
                g.Clear(System.Drawing.Color.Red);
            }

            using (MemoryStream imgStream = new MemoryStream())
            {
                bmp.Save(imgStream, ImageFormat.Png);
                imgStream.Position = 0;

                // Build a PDF and embed the image.
                Document doc = new Document();
                Page page = doc.Pages.Add();
                Aspose.Pdf.Image pdfImg = new Aspose.Pdf.Image
                {
                    ImageStream = imgStream
                };
                page.Paragraphs.Add(pdfImg);
                doc.Save(outputPath);
            }
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal stubs for the AWS SDK (Amazon.S3) to allow the sample to compile
// when the real SDK is not referenced. In a production project, replace these
// stubs with the official NuGet package "AWSSDK.S3".
// ---------------------------------------------------------------------------
namespace Amazon
{
    public class RegionEndpoint
    {
        public string SystemName { get; }
        private RegionEndpoint(string systemName) => SystemName = systemName;
        public static RegionEndpoint USEast1 => new RegionEndpoint("us-east-1");
    }
}

namespace Amazon.S3
{
    using System.Threading.Tasks;
    using Amazon.S3.Model;

    public class AmazonS3Client
    {
        private readonly RegionEndpoint _region;
        public AmazonS3Client(RegionEndpoint region) => _region = region;
        public Task PutObjectAsync(PutObjectRequest request) => Task.CompletedTask; // Stub
    }
}

namespace Amazon.S3.Model
{
    using System.IO;

    public class PutObjectRequest
    {
        public string? BucketName { get; set; }
        public string? Key { get; set; }
        public Stream? InputStream { get; set; }
        public string? ContentType { get; set; }
    }
}
