using System;
using System.IO;
using Aspose.Pdf.Facades;               // PdfExtractor resides here
using Google.Cloud.Storage.V1;          // Google Cloud Storage client (stubbed if package missing)
using Google.Apis.Storage.v1.Data;      // Predefined ACL enum (stubbed if package missing)

// ---------------------------------------------------------------------------
// Stub implementations for Google Cloud Storage types.
// These are only compiled when the real Google.Cloud.Storage.V1 package is not
// referenced. In a production environment you should add the NuGet package
// "Google.Cloud.Storage.V1" (which brings in Google.Apis.Storage.v1.Data) and
// remove these stubs.
// ---------------------------------------------------------------------------
namespace Google.Apis.Storage.v1.Data
{
    /// <summary>
    /// Minimal stub of the PredefinedObjectAcl enum used by the real Google API.
    /// </summary>
    public enum PredefinedObjectAcl
    {
        Private,
        PublicRead,
        PublicReadWrite,
        AuthenticatedRead
    }
}

namespace Google.Cloud.Storage.V1
{
    /// <summary>
    /// Options for uploading an object. Mirrors the real UploadObjectOptions class.
    /// </summary>
    public class UploadObjectOptions
    {
        public PredefinedObjectAcl PredefinedAcl { get; set; } = PredefinedObjectAcl.Private;
    }

    /// <summary>
    /// Very small stub of the real StorageClient. In production you should use the
    /// real client from the Google.Cloud.Storage.V1 package. This stub simply writes
    /// the uploaded stream to a local "gcs-mock" folder to allow the code to compile
    /// and run without external dependencies.
    /// </summary>
    public class StorageClient
    {
        /// <summary>
        /// Creates a new instance of the stub client.
        /// </summary>
        public static StorageClient Create() => new StorageClient();

        /// <summary>
        /// Uploads an object to a bucket. The stub writes the stream to a local folder
        /// named "gcs-mock/{bucketName}" preserving the object name.
        /// </summary>
        public void UploadObject(string bucket, string objectName, string contentType, Stream source, UploadObjectOptions options = null)
        {
            // Ensure the mock bucket directory exists.
            string mockRoot = Path.Combine(Directory.GetCurrentDirectory(), "gcs-mock", bucket);
            Directory.CreateDirectory(mockRoot);

            string filePath = Path.Combine(mockRoot, objectName);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                source.CopyTo(fileStream);
            }

            // Log the simulated upload for visibility.
            Console.WriteLine($"[Mock GCS] Uploaded '{objectName}' to bucket '{bucket}' (ACL: {options?.PredefinedAcl ?? PredefinedObjectAcl.Private})");
        }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Path to source PDF
        const string bucketName = "my-gcs-bucket";   // Target GCS bucket (must exist in real GCS)

        // Verify PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Create a Google Cloud Storage client.
        // Authentication is handled via GOOGLE_APPLICATION_CREDENTIALS env variable or default credentials.
        // When using the real Google.Cloud.Storage.V1 package, this will talk to GCS.
        // With the stub above it writes to a local folder for demonstration purposes.
        StorageClient storageClient = StorageClient.Create();

        // Use PdfExtractor to pull images from the PDF.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to pull images.
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate over all extracted images.
            while (extractor.HasNextImage())
            {
                // Store the current image in a memory stream.
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Retrieve the next image; returns true if successful.
                    bool success = extractor.GetNextImage(imageStream);
                    if (!success)
                    {
                        Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                        break;
                    }

                    // Reset stream position before upload.
                    imageStream.Position = 0;

                    // Define an object name for the uploaded image.
                    // The default format from GetNextImage is JPEG; adjust extension if needed.
                    string objectName = $"image-{imageIndex}.jpg";

                    // Upload the image to GCS with public read access.
                    storageClient.UploadObject(
                        bucket: bucketName,
                        objectName: objectName,
                        contentType: null, // Let GCS infer the MIME type.
                        source: imageStream,
                        options: new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead });

                    Console.WriteLine($"Uploaded {objectName} to bucket {bucketName} (publicly readable).");
                }

                imageIndex++;
            }
        }
    }
}
