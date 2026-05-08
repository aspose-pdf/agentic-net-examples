using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfExtractor
using Aspose.Pdf;                 // ImageFormat
using Azure.Storage.Blobs;        // BlobContainerClient, BlobClient

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs when the real NuGet package is not
// referenced. They allow the sample to compile and run (the Upload method
// simply writes the stream to a local file in the current directory).
// Remove these stubs and add the official Azure.Storage.Blobs package for
// production use.
// ---------------------------------------------------------------------------
namespace Azure.Storage.Blobs
{
    public class BlobContainerClient
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        public BlobContainerClient(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }
        public void CreateIfNotExists()
        {
            // No‑op stub – in real code this creates the container if missing.
        }
        public BlobClient GetBlobClient(string blobName) => new BlobClient(_containerName, blobName);
    }

    public class BlobClient
    {
        private readonly string _containerName;
        private readonly string _blobName;
        public BlobClient(string containerName, string blobName)
        {
            _containerName = containerName;
            _blobName = blobName;
        }
        public void Upload(Stream content, bool overwrite = false)
        {
            // Simple stub – writes the stream to a file named <blobName> in the
            // current working directory. Real implementation uploads to Azure.
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), _blobName);
            using (var file = new FileStream(filePath, overwrite ? FileMode.Create : FileMode.CreateNew))
            {
                content.CopyTo(file);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Azure Blob Storage connection details (stub values – replace with real ones)
        const string azureConnectionString = "UseDevelopmentStorage=true"; // placeholder
        const string containerName = "pdf-images";

        // Validate input file existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize Azure Blob container client (stub implementation works without the real SDK)
        BlobContainerClient containerClient = new BlobContainerClient(azureConnectionString, containerName);
        containerClient.CreateIfNotExists();

        try
        {
            // Initialize PdfExtractor and bind the PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractImage(); // Prepare image extraction

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream (default format is JPEG)
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        // Optionally specify a format, e.g., PNG:
                        // extractor.GetNextImage(imageStream, ImageFormat.Png);
                        extractor.GetNextImage(imageStream); // uses default format
                        imageStream.Position = 0; // Reset stream position for upload

                        // Define a unique blob name for each image
                        string blobName = $"image-{imageIndex}.jpg";

                        // Upload the image stream to Azure Blob Storage (stub writes to local file)
                        BlobClient blobClient = containerClient.GetBlobClient(blobName);
                        blobClient.Upload(imageStream, overwrite: true);
                    }

                    Console.WriteLine($"Uploaded image {imageIndex}");
                    imageIndex++;
                }
            }

            Console.WriteLine("All images extracted and uploaded successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
