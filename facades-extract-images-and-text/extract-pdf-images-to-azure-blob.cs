using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfExtractor resides here
using Azure.Storage.Blobs;        // Azure Blob SDK (stub implementation provided below)

// ---------------------------------------------------------------------------
// Minimal stub implementation of the Azure.Storage.Blobs SDK.
// This allows the sample to compile without adding the real NuGet package.
// In production replace this stub with the official Azure.Storage.Blobs package.
// ---------------------------------------------------------------------------
namespace Azure.Storage.Blobs
{
    public class BlobServiceClient
    {
        private readonly string _connectionString;
        public BlobServiceClient(string connectionString) => _connectionString = connectionString;
        public BlobContainerClient GetBlobContainerClient(string containerName) => new BlobContainerClient(containerName);
    }

    public class BlobContainerClient
    {
        private readonly string _containerName;
        public BlobContainerClient(string containerName) => _containerName = containerName;
        public void CreateIfNotExists() { /* No‑op for stub */ }
        public BlobClient GetBlobClient(string blobName) => new BlobClient(blobName);
    }

    public class BlobClient
    {
        private readonly string _blobName;
        public BlobClient(string blobName) => _blobName = blobName;
        public void Upload(Stream content, bool overwrite = false)
        {
            // In a real implementation this would upload to Azure Blob Storage.
            // The stub simply discards the data.
        }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                     // source PDF
        const string connectionString = "YourAzureBlobConnectionString"; // Azure storage connection
        const string containerName = "pdf-images";              // target container

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize Azure Blob container (create if it does not exist)
        BlobServiceClient serviceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
        containerClient.CreateIfNotExists();

        // Use PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);   // bind the PDF file
            extractor.ExtractImage();     // start image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream (default format is JPEG)
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0; // reset for upload

                    // Upload the image to Azure Blob storage
                    string blobName = $"image-{imageIndex}.jpg";
                    BlobClient blobClient = containerClient.GetBlobClient(blobName);
                    blobClient.Upload(imageStream, overwrite: true);
                    Console.WriteLine($"Uploaded {blobName}");
                }

                imageIndex++;
            }
        }

        Console.WriteLine("All images extracted and uploaded successfully.");
    }
}
