using System;
using System.IO;
using Aspose.Pdf.Facades;
using Google.Cloud.Storage.V1;

// ---------------------------------------------------------------------------
// Minimal stubs for Google.Cloud.Storage.V1 (used when the real NuGet package
// is not referenced). These provide just enough members to compile the sample
// program and demonstrate the intended behaviour (uploading to GCS). In a real
// project you should reference the official Google.Cloud.Storage.V1 package.
// ---------------------------------------------------------------------------
namespace Google.Cloud.Storage.V1
{
    /// <summary>
    /// Represents the predefined ACL values for an uploaded object.
    /// Only the values required by this sample are defined.
    /// </summary>
    public enum PredefinedObjectAcl
    {
        PublicRead
    }

    /// <summary>
    /// Options that can be supplied to <see cref="StorageClient.UploadObject"/>.
    /// </summary>
    public class UploadObjectOptions
    {
        public PredefinedObjectAcl PredefinedAcl { get; set; }
    }

    /// <summary>
    /// Very small mock of the Google Cloud Storage client. The real client
    /// communicates with GCS; this stub simply writes the uploaded data to the
    /// console so the sample can be compiled and run without external
    /// dependencies.
    /// </summary>
    public class StorageClient
    {
        public static StorageClient Create() => new StorageClient();

        public void UploadObject(
            string bucket,
            string objectName,
            string contentType,
            Stream source,
            UploadObjectOptions options = null)
        {
            // In a real implementation the stream would be sent to GCS.
            // Here we just read the stream length and report the upload.
            long length = 0;
            if (source.CanSeek)
            {
                length = source.Length;
            }
            else
            {
                // Fallback for non‑seekable streams.
                using var ms = new MemoryStream();
                source.CopyTo(ms);
                length = ms.Length;
            }

            Console.WriteLine($"[Stub] Uploaded {objectName} ({contentType}, {length} bytes) to bucket '{bucket}' with ACL={options?.PredefinedAcl}");
        }
    }
}

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string pdfPath = "input.pdf";

        // Google Cloud Storage bucket name (must already exist).
        const string bucketName = "my-public-bucket";

        // Verify the PDF file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Create a Google Cloud Storage client (uses default credentials).
        StorageClient storageClient = StorageClient.Create();

        // Use PdfExtractor to pull images from the PDF.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file.
            extractor.BindPdf(pdfPath);

            // Extract images (you can change the mode if needed).
            // extractor.ExtractImageMode = Aspose.Pdf.ExtractImageMode.ActuallyUsed;
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream.
                using (MemoryStream imageStream = new MemoryStream())
                {
                    bool success = extractor.GetNextImage(imageStream);
                    if (!success)
                    {
                        Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                        break;
                    }

                    // Reset stream position before upload.
                    imageStream.Position = 0;

                    // Define an object name for the image in the bucket.
                    string objectName = $"image-{imageIndex}.png";

                    // Upload the image to GCS with public read access.
                    // The content type is set to PNG; adjust if you know the actual format.
                    storageClient.UploadObject(
                        bucket: bucketName,
                        objectName: objectName,
                        contentType: "image/png",
                        source: imageStream,
                        options: new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead });

                    Console.WriteLine($"Uploaded {objectName} to bucket {bucketName} (public).");
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and upload completed.");
    }
}
