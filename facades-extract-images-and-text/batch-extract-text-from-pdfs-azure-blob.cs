using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point
    static async Task Main(string[] args)
    {
        // Azure Blob storage configuration
        string connectionString = "<YOUR_AZURE_BLOB_CONNECTION_STRING>";
        string containerName    = "<YOUR_CONTAINER_NAME>";

        // Create a client for the container
        BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

        // Iterate over all PDF blobs in the container
        await foreach (BlobItem blobItem in container.GetBlobsAsync())
        {
            if (!blobItem.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                continue; // Skip non‑PDF files

            // Download the PDF to a temporary file (required for Aspose.Pdf usage)
            string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            BlobClient pdfBlob = container.GetBlobClient(blobItem.Name);
            await pdfBlob.DownloadToAsync(tempPdfPath);

            try
            {
                // Load the PDF with Aspose.Pdf Document (lifecycle rule)
                using (Document doc = new Document(tempPdfPath))
                {
                    // Get page count directly from the Document (PdfFileEditor has no GetPageCount instance method)
                    int pageCount = doc.Pages.Count;
                    Console.WriteLine($"Processing '{blobItem.Name}' – {pageCount} pages.");

                    // Extract all text using TextAbsorber (recommended API)
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);
                    string extractedText = absorber.Text ?? string.Empty;

                    // Prepare the name for the text result blob
                    string textBlobName = Path.ChangeExtension(blobItem.Name, ".txt");
                    BlobClient textBlob = container.GetBlobClient(textBlobName);

                    // Upload the extracted text back to the container
                    using (MemoryStream txtStream = new MemoryStream())
                    using (StreamWriter writer = new StreamWriter(txtStream))
                    {
                        writer.Write(extractedText);
                        writer.Flush();
                        txtStream.Position = 0;
                        await textBlob.UploadAsync(txtStream, overwrite: true);
                    }

                    Console.WriteLine($"Uploaded extracted text to '{textBlobName}'.");
                }
            }
            finally
            {
                // Clean up the temporary PDF file
                if (File.Exists(tempPdfPath))
                    File.Delete(tempPdfPath);
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs types to allow compilation without the
// actual NuGet package. In a real project you should reference the
// Azure.Storage.Blobs NuGet package instead of using these stubs.
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

        public BlobClient GetBlobClient(string blobName) => new BlobClient(_connectionString, _containerName, blobName);

        public async IAsyncEnumerable<BlobItem> GetBlobsAsync()
        {
            // Stub implementation – returns an empty sequence.
            await Task.CompletedTask;
            yield break;
        }
    }

    public class BlobClient
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly string _blobName;

        public BlobClient(string connectionString, string containerName, string blobName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
            _blobName = blobName;
        }

        public Task DownloadToAsync(string path)
        {
            // Stub – in real code this would download the blob.
            // Here we just create an empty file so the rest of the code can run.
            File.WriteAllBytes(path, Array.Empty<byte>());
            return Task.CompletedTask;
        }

        public Task UploadAsync(Stream content, bool overwrite = false)
        {
            // Stub – in real code this would upload the stream.
            // No operation needed for compilation.
            return Task.CompletedTask;
        }
    }
}

namespace Azure.Storage.Blobs.Models
{
    public class BlobItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
