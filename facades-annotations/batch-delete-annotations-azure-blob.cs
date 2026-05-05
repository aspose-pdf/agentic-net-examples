using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Azure Blob Storage connection details
        const string connectionString = "<YOUR_AZURE_STORAGE_CONNECTION_STRING>";
        const string sourceContainerName = "pdf-source";
        const string targetContainerName = "pdf-processed";

        // Initialize source and target containers
        BlobContainerClient sourceContainer = new BlobContainerClient(connectionString, sourceContainerName);
        BlobContainerClient targetContainer = new BlobContainerClient(connectionString, targetContainerName);
        targetContainer.CreateIfNotExists();

        // Iterate over each PDF blob in the source container
        foreach (BlobItem blobItem in sourceContainer.GetBlobs())
        {
            // Process only blobs with .pdf extension (case‑insensitive)
            if (!blobItem.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                continue;

            BlobClient sourceBlob = sourceContainer.GetBlobClient(blobItem.Name);
            BlobClient targetBlob = targetContainer.GetBlobClient(blobItem.Name);

            try
            {
                // Download the PDF into a memory stream
                using (MemoryStream inputStream = new MemoryStream())
                {
                    sourceBlob.DownloadTo(inputStream);
                    inputStream.Position = 0; // Reset for reading

                    // Bind the stream to PdfAnnotationEditor
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(inputStream);

                        // Example batch operation: delete all annotations in the document
                        editor.DeleteAnnotations();

                        // Save the modified PDF into an output stream
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            editor.Save(outputStream);
                            outputStream.Position = 0; // Reset for upload

                            // Upload the processed PDF back to Azure Blob Storage
                            targetBlob.Upload(outputStream, overwrite: true);
                        }
                    }
                }

                Console.WriteLine($"Processed blob: {blobItem.Name}");
            }
            catch (RequestFailedException ex)
            {
                Console.Error.WriteLine($"Azure error processing '{blobItem.Name}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"General error processing '{blobItem.Name}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}

// ---------------------------------------------------------------------------
// Minimal stubs for Azure SDK types – remove these when the real Azure.Storage.Blobs
// NuGet package is referenced in the project (recommended for production).
// ---------------------------------------------------------------------------
namespace Azure
{
    public class RequestFailedException : Exception
    {
        public RequestFailedException(string message) : base(message) { }
    }
}

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
        public void CreateIfNotExists() { /* No‑op stub */ }
        public IEnumerable<BlobItem> GetBlobs() => Enumerable.Empty<BlobItem>();
        public BlobClient GetBlobClient(string blobName) => new BlobClient();
    }

    public class BlobClient
    {
        public void DownloadTo(Stream destination) { /* No‑op stub – in real code this streams the blob */ }
        public void Upload(Stream source, bool overwrite) { /* No‑op stub – in real code this uploads the blob */ }
    }
}

namespace Azure.Storage.Blobs.Models
{
    public class BlobItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
