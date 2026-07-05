using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // Aspose PDF Facade classes

// ---------------------------------------------------------------------------
// Minimal stub implementation for Google Cloud Storage client to allow the code
// to compile without adding the external NuGet package. In a real project you
// should reference the official "Google.Cloud.Storage.V1" package and remove
// this stub.
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
        // Factory method that mimics the real SDK.
        public static StorageClient Create() => new StorageClient();

        // Simplified overload used in the sample code.
        // Made the options parameter nullable to satisfy nullable reference types.
        public void UploadObject(string bucket, string objectName, string contentType, Stream source, UploadObjectOptions? options = null)
        {
            // In a production scenario this method would upload the stream to GCS.
            // Here we just simulate the upload to keep the sample self‑contained.
            Console.WriteLine($"[Stub] Uploading '{objectName}' to bucket '{bucket}' with content type '{contentType}'. ACL: {options?.PredefinedAcl}");
            // Reset the stream position to mimic consumption.
            source.Position = 0;
        }
    }
}

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Ensure a PDF exists so the sample runs without external files.
        // In a real scenario you would provide your own PDF.
        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPdfPath);
                Console.WriteLine($"Created placeholder PDF at '{inputPdfPath}'.");
            }
        }

        // Google Cloud Storage bucket name (must already exist)
        const string bucketName = "my-public-bucket";

        // Optional: prefix folder inside the bucket
        const string bucketFolder = "extracted-images";

        // Create a Google Cloud Storage client (uses default credentials)
        // NOTE: In a real implementation reference the NuGet package "Google.Cloud.Storage.V1"
        var storageClient = Google.Cloud.Storage.V1.StorageClient.Create();

        // Initialize the PDF extractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract images from the PDF (default mode extracts all defined resources)
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Store the image in a memory stream (default format is JPEG)
                using (MemoryStream imageStream = new MemoryStream())
                {
                    bool extracted = extractor.GetNextImage(imageStream);
                    if (!extracted)
                    {
                        // If extraction failed, skip to the next image
                        continue;
                    }

                    // Reset stream position before uploading
                    imageStream.Position = 0;

                    // Build the object name (e.g., "extracted-images/image-1.jpg")
                    string objectName = $"{bucketFolder}/image-{imageIndex}.jpg";

                    // Upload the image to GCS with public read access
                    storageClient.UploadObject(
                        bucket: bucketName,
                        objectName: objectName,
                        contentType: "image/jpeg",
                        source: imageStream,
                        options: new Google.Cloud.Storage.V1.UploadObjectOptions
                        {
                            // Make the uploaded object publicly readable
                            PredefinedAcl = Google.Cloud.Storage.V1.PredefinedObjectAcl.PublicRead
                        });

                    Console.WriteLine($"Uploaded {objectName} to bucket {bucketName} (public).");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction and upload completed.");
    }
}
