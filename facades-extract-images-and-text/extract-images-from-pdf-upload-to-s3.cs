using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

// Minimal stubs for AWS SDK types to allow compilation without the actual package.
namespace Amazon
{
    public class RegionEndpoint
    {
        public static readonly RegionEndpoint USEast1 = new RegionEndpoint("us-east-1");
        public string SystemName { get; }
        private RegionEndpoint(string name) => SystemName = name;
    }

    namespace S3
    {
        public class AmazonS3Client
        {
            private readonly RegionEndpoint _region;
            public AmazonS3Client(RegionEndpoint region) => _region = region;

            // In a real implementation this would upload to S3. Here we just return a completed task.
            public Task<PutObjectResponse> PutObjectAsync(PutObjectRequest request)
            {
                // Simulate async upload latency if desired.
                return Task.FromResult(new PutObjectResponse());
            }
        }

        public class PutObjectRequest
        {
            // Made nullable to satisfy the non‑nullable reference type warnings.
            public string? BucketName { get; set; }
            public string? Key { get; set; }
            public Stream? InputStream { get; set; }
            public string? ContentType { get; set; }
        }

        public class PutObjectResponse { }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                 // Path to source PDF
        const string bucketName = "my-s3-bucket";           // Target S3 bucket
        const string keyPrefix = "extracted-images/";       // Folder inside bucket
        var s3Client = new Amazon.S3.AmazonS3Client(Amazon.RegionEndpoint.USEast1);

        // ---------------------------------------------------------------------
        // 1️⃣ Create a sample PDF that contains at least one image.
        // ---------------------------------------------------------------------
        CreateSamplePdfWithImage(pdfPath);

        // ---------------------------------------------------------------------
        // 2️⃣ Extract images from the PDF and upload each to S3.
        // ---------------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // Load the PDF
            extractor.ExtractImage();            // Prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Retrieve next image (default JPEG format) into the stream
                    bool success = extractor.GetNextImage(imageStream);
                    if (!success)
                    {
                        Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                        break;
                    }

                    // Reset stream position before upload
                    imageStream.Position = 0;

                    // Build S3 object key (e.g., extracted-images/1.jpg)
                    string objectKey = $"{keyPrefix}{imageIndex}.jpg";

                    var putRequest = new Amazon.S3.PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = objectKey,
                        InputStream = imageStream,
                        ContentType = "image/jpeg"
                    };

                    // Upload to S3 (synchronous wait)
                    s3Client.PutObjectAsync(putRequest).GetAwaiter().GetResult();

                    Console.WriteLine($"Uploaded image #{imageIndex} to s3://{bucketName}/{objectKey}");
                }

                imageIndex++;
            }
        }
    }

    /// <summary>
    /// Generates a simple PDF containing a single JPEG image. The PDF is saved to <paramref name="path"/>.
    /// This makes the example self‑contained and runnable in the sandbox where no external files exist.
    /// </summary>
    private static void CreateSamplePdfWithImage(string path)
    {
        // Create a 100x100 red bitmap and encode it as JPEG in memory.
        using (Bitmap bmp = new Bitmap(100, 100))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color.
                g.Clear(System.Drawing.Color.Red);
            }

            using (MemoryStream imgStream = new MemoryStream())
            {
                bmp.Save(imgStream, ImageFormat.Jpeg);
                imgStream.Position = 0;

                // Build the PDF and embed the image.
                Document doc = new Document();
                Page page = doc.Pages.Add();
                Aspose.Pdf.Image pdfImg = new Aspose.Pdf.Image
                {
                    ImageStream = new MemoryStream(imgStream.ToArray())
                };
                page.Paragraphs.Add(pdfImg);
                doc.Save(path);
            }
        }
    }
}
