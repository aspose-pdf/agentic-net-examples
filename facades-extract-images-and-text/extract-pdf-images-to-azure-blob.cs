using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfExtractor
using Azure.Storage.Blobs;          // BlobContainerClient, BlobClient

// ---------------------------------------------------------------------------
// Minimal stub definitions for Azure.Storage.Blobs types (used when the NuGet
// package is not referenced). In a real project you should add the
// Azure.Storage.Blobs NuGet package instead of these stubs.
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

        // In the real SDK this creates the container if it does not exist.
        // Here it is a no‑op placeholder.
        public void CreateIfNotExists() { }

        public BlobClient GetBlobClient(string blobName) => new BlobClient(blobName);
    }

    public class BlobClient
    {
        private readonly string _blobName;

        public BlobClient(string blobName = null)
        {
            _blobName = blobName;
        }

        // In the real SDK this uploads the stream to Azure Blob storage.
        // The stub simply reads the stream to ensure it is consumable.
        public void Upload(Stream stream, bool overwrite = false)
        {
            // Consume the stream so that any errors surface early.
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            // No actual upload performed.
        }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string azureConnectionString = "<YOUR_AZURE_STORAGE_CONNECTION_STRING>";
        const string containerName = "pdf-images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize Azure Blob container client (creates container if it does not exist)
        BlobContainerClient containerClient = new BlobContainerClient(azureConnectionString, containerName);
        containerClient.CreateIfNotExists();

        // Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // Load the PDF
            extractor.ExtractImage();            // Prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream); // Write image to stream
                    imageStream.Position = 0;            // Reset stream position for upload

                    // Generate a unique blob name for each image
                    string blobName = $"image_{imageIndex}.bin";

                    // Upload the image stream to Azure Blob storage
                    BlobClient blobClient = containerClient.GetBlobClient(blobName);
                    blobClient.Upload(imageStream, overwrite: true);
                }

                imageIndex++;
            }
        }

        Console.WriteLine("All images extracted and uploaded to Azure Blob storage.");
    }
}
