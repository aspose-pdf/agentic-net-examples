using System;
using System.IO;
using Aspose.Pdf.Facades;
using Google.Cloud.Storage.V1;

// Stub definitions for Google.Cloud.Storage.V1 (remove when the real NuGet package is referenced)
namespace Google.Cloud.Storage.V1
{
    public enum PredefinedObjectAcl
    {
        Private,
        PublicRead
    }

    public class UploadObjectOptions
    {
        public PredefinedObjectAcl PredefinedAcl { get; set; }
    }

    public class StorageClient
    {
        public static StorageClient Create() => new StorageClient();

        // Made the options parameter nullable to satisfy nullable reference type analysis
        public void UploadObject(string bucket, string objectName, string contentType, Stream source, UploadObjectOptions? options = null)
        {
            // Stub implementation – in a real scenario this would upload to GCS.
            // Consume the stream so callers can safely dispose it afterwards.
            using var ms = new MemoryStream();
            source.CopyTo(ms);
            Console.WriteLine($"[Stub] Uploaded {objectName} to bucket {bucket} with content type {contentType} and ACL {options?.PredefinedAcl}");
        }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string bucketName = "my-gcs-bucket";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize Google Cloud Storage client (stub or real implementation)
        StorageClient storageClient = StorageClient.Create();

        // Extract images using Aspose.Pdf.Facades.PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(pdfPath);

            // NOTE: The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
            // The default behavior extracts all images, so no explicit mode setting is required.

            // Extract all images from the PDF
            extractor.ExtractImage();

            int imageIndex = 0;
            while (extractor.HasNextImage())
            {
                imageIndex++;
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Retrieve the next image into the memory stream
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0; // reset for reading

                    // Determine MIME type (fallback to generic if unknown)
                    string contentType = GetImageContentType(imageStream) ?? "application/octet-stream";

                    // Build a unique object name for GCS
                    string objectName = $"image_{imageIndex}{GetExtensionFromContentType(contentType)}";

                    // Upload the image to GCS with public read access
                    storageClient.UploadObject(
                        bucket: bucketName,
                        objectName: objectName,
                        contentType: contentType,
                        source: imageStream,
                        options: new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead });

                    Console.WriteLine($"Uploaded {objectName} to bucket {bucketName} (public).");
                }
            }
        }
    }

    // Simple MIME type detection based on file header bytes
    // Return type is nullable because the method may not recognise the format.
    static string? GetImageContentType(Stream stream)
    {
        byte[] header = new byte[8];
        int bytesRead = stream.Read(header, 0, header.Length);
        stream.Position = 0; // reset after reading

        if (bytesRead >= 8)
        {
            // PNG
            if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
                return "image/png";
            // JPEG
            if (header[0] == 0xFF && header[1] == 0xD8)
                return "image/jpeg";
            // GIF
            if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46)
                return "image/gif";
            // BMP
            if (header[0] == 0x42 && header[1] == 0x4D)
                return "image/bmp";
        }
        return null;
    }

    // Map MIME type to appropriate file extension
    static string GetExtensionFromContentType(string contentType) =>
        contentType switch
        {
            "image/png" => ".png",
            "image/jpeg" => ".jpg",
            "image/gif" => ".gif",
            "image/bmp" => ".bmp",
            _ => ""
        };
}
