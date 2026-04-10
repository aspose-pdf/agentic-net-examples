using System;
using System.IO;
using Aspose.Pdf.Facades;
using Google.Cloud.Storage.V1;
using Google.Apis.Storage.v1.Data;

// ---------------------------------------------------------------------------
// Minimal stubs for Google Cloud Storage client types when the real NuGet
// packages are not referenced. These allow the sample to compile and run in a
// sandboxed environment. Replace them with the official packages for production.
// ---------------------------------------------------------------------------
namespace Google.Cloud.Storage.V1
{
    public class StorageClient
    {
        public static StorageClient Create() => new StorageClient();

        public void UploadObject(string bucket, string objectName, string contentType, Stream source, UploadObjectOptions options = null)
        {
            // Stub implementation – writes the uploaded data to a local file so the
            // example can be executed without real GCS access.
            string safeName = objectName.Replace('/', '_');
            using var file = File.Create(safeName);
            source.CopyTo(file);
            Console.WriteLine($"[Stub] Uploaded to bucket '{bucket}' as '{objectName}' (content-type: {contentType}, acl: {options?.PredefinedAcl})");
        }
    }

    public class UploadObjectOptions
    {
        public PredefinedObjectAcl PredefinedAcl { get; set; }
    }
}

namespace Google.Apis.Storage.v1.Data
{
    public enum PredefinedObjectAcl
    {
        PublicRead,
        Private
    }
}

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string pdfPath = "input.pdf";

        // Google Cloud Storage bucket name (must already exist).
        const string bucketName = "my-gcs-bucket";

        // Optional prefix for uploaded objects.
        const string objectPrefix = "extracted-images/";

        // Create a Google Cloud Storage client using default credentials (stub).
        StorageClient storageClient = StorageClient.Create();

        // Ensure the PDF file exists before processing.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Use PdfExtractor to extract images from the PDF.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(pdfPath);

                // Extract all images defined in the PDF resources.
                extractor.ExtractImage();

                int imageIndex = 1;

                // Iterate through all extracted images.
                while (extractor.HasNextImage())
                {
                    // Store the image in a memory stream (default format is JPEG).
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        // Retrieve the next image into the stream.
                        extractor.GetNextImage(imageStream);

                        // Reset stream position before uploading.
                        imageStream.Position = 0;

                        // Build the object name for GCS (e.g., extracted-images/image-1.jpg).
                        string objectName = $"{objectPrefix}image-{imageIndex}.jpg";

                        // Upload the image to GCS with public read access.
                        storageClient.UploadObject(
                            bucket: bucketName,
                            objectName: objectName,
                            contentType: "image/jpeg",
                            source: imageStream,
                            options: new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead });

                        Console.WriteLine($"Uploaded {objectName} to bucket {bucketName}");
                    }

                    imageIndex++;
                }
            }

            Console.WriteLine("Image extraction and upload completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
