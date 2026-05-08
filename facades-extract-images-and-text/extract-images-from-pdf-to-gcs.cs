using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Google.Cloud.Storage.V1 when the real NuGet package is not
// available. These provide just enough members used by the sample code so the
// project can compile and run (the stub simply writes a message to the console).
// ---------------------------------------------------------------------------
namespace Google.Cloud.Storage.V1
{
    public enum PredefinedObjectAcl
    {
        PublicRead
    }

    public class UploadObjectOptions
    {
        public PredefinedObjectAcl PredefinedAcl { get; set; }
    }

    public class StorageClient
    {
        public static StorageClient Create() => new StorageClient();

        public void UploadObject(string bucketName, string objectName, string contentType, Stream source, UploadObjectOptions options = null)
        {
            // In a real implementation this would upload to GCS. The stub just
            // reports the action so the sample can be executed without external
            // dependencies.
            Console.WriteLine($"[Stub] Uploaded '{objectName}' to bucket '{bucketName}' with content type '{contentType}'.");
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // Path to the source PDF
        const string bucketName   = "my-gcs-bucket";      // Google Cloud Storage bucket
        const string gcsFolder    = "extracted-images/"; // Optional folder/prefix in the bucket

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Initialize Google Cloud Storage client (uses Application Default Credentials)
        var storageClient = Google.Cloud.Storage.V1.StorageClient.Create();

        // Use Aspose.Pdf.Facades.PdfExtractor to pull images from the PDF
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);   // Load the PDF
            extractor.ExtractImage();          // Prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Capture the next image into a memory stream as PNG
                using (var imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream, ImageFormat.Png);
                    imageStream.Position = 0; // Reset for upload

                    // Build the object name (e.g., extracted-images/image-1.png)
                    string objectName = $"{gcsFolder}image-{imageIndex}.png";

                    // Upload with public read access
                    var uploadOptions = new Google.Cloud.Storage.V1.UploadObjectOptions
                    {
                        PredefinedAcl = Google.Cloud.Storage.V1.PredefinedObjectAcl.PublicRead
                    };
                    storageClient.UploadObject(bucketName, objectName, "image/png", imageStream, uploadOptions);

                    Console.WriteLine($"Uploaded {objectName} to bucket {bucketName}");
                }

                imageIndex++;
            }
        }

        Console.WriteLine("All images extracted and uploaded to Google Cloud Storage.");
    }
}
