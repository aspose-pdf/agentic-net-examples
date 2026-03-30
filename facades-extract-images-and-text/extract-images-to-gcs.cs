using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

// ---------------------------------------------------------------------------
// Stubs for Google Cloud Storage SDK (used when the real NuGet packages are not
// referenced). In a production environment you should add the NuGet packages:
//   Google.Cloud.Storage.V1
//   Google.Apis.Storage.v1
// and remove the stub implementations below.
// ---------------------------------------------------------------------------
namespace Google.Cloud.Storage.V1
{
    public enum PredefinedObjectAcl
    {
        Private,
        PublicRead,
        PublicReadWrite,
        AuthenticatedRead,
        BucketOwnerRead,
        BucketOwnerFullControl,
        ProjectPrivate
    }

    public class UploadObjectOptions
    {
        public PredefinedObjectAcl PredefinedAcl { get; set; } = PredefinedObjectAcl.Private;
    }

    public class StorageClient
    {
        // Factory method matching the real SDK.
        public static StorageClient Create() => new StorageClient();

        // Minimal implementation that writes the uploaded object to a local folder
        // named "gcs-mock" to simulate a bucket. In real code this method uploads to
        // Google Cloud Storage.
        public void UploadObject(string bucketName, string objectName, string contentType, Stream source, UploadObjectOptions options = null)
        {
            if (options == null) options = new UploadObjectOptions();

            string mockRoot = Path.Combine(Directory.GetCurrentDirectory(), "gcs-mock");
            string bucketPath = Path.Combine(mockRoot, bucketName);
            Directory.CreateDirectory(bucketPath);
            string destinationPath = Path.Combine(bucketPath, objectName);

            using (var fileStream = File.Create(destinationPath))
            {
                source.CopyTo(fileStream);
            }

            // Simulate public read by setting a simple flag file (for demo only).
            if (options.PredefinedAcl == PredefinedObjectAcl.PublicRead)
            {
                File.WriteAllText(destinationPath + ".public", "public");
            }

            Console.WriteLine($"[Mock GCS] Uploaded '{objectName}' to bucket '{bucketName}' (PublicRead={options.PredefinedAcl == PredefinedObjectAcl.PublicRead})");
        }
    }
}

// The Google.Apis.Storage.v1.Data namespace is referenced only for type
// resolution; we provide an empty placeholder to satisfy the compiler.
namespace Google.Apis.Storage.v1.Data { }

class Program
{
    static void Main()
    {
        string inputPdfPath = "input.pdf";
        string bucketName = "my-gcs-bucket";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdfPath);
            return;
        }

        // Initialize Google Cloud Storage client.
        var storageClient = Google.Cloud.Storage.V1.StorageClient.Create();

        // Extract images from the PDF.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string tempFileName = $"image-{imageIndex}.png";
                // Save the extracted image as PNG.
                extractor.GetNextImage(tempFileName, ImageFormat.Png);

                // Upload the image to Google Cloud Storage with public read access.
                using (FileStream fileStream = File.OpenRead(tempFileName))
                {
                    storageClient.UploadObject(
                        bucketName,
                        tempFileName,
                        null, // content type – let GCS infer or set explicitly if needed
                        fileStream,
                        new Google.Cloud.Storage.V1.UploadObjectOptions { PredefinedAcl = Google.Cloud.Storage.V1.PredefinedObjectAcl.PublicRead }
                    );
                }

                // Remove the temporary local file.
                File.Delete(tempFileName);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and upload completed.");
    }
}
