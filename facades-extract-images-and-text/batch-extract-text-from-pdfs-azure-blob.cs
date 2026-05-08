using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Aspose.Pdf.Facades; // PdfExtractor resides here

class Program
{
    // Adjust these constants as needed.
    private const string BlobConnectionString = "<YOUR_AZURE_BLOB_CONNECTION_STRING>";
    private const string InputContainerName   = "pdf-input";
    private const string OutputContainerName  = "text-output";

    static async Task Main()
    {
        // Initialize blob service client.
        BlobServiceClient serviceClient = new BlobServiceClient(BlobConnectionString);
        BlobContainerClient inputContainer  = serviceClient.GetBlobContainerClient(InputContainerName);
        BlobContainerClient outputContainer = serviceClient.GetBlobContainerClient(OutputContainerName);

        // Ensure the output container exists.
        await outputContainer.CreateIfNotExistsAsync();

        // List all PDF blobs in the input container.
        await foreach (BlobItem blobItem in inputContainer.GetBlobsAsync())
        {
            if (!blobItem.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                continue; // Skip non‑PDF files.

            Console.WriteLine($"Processing: {blobItem.Name}");

            // Download the PDF into a memory stream.
            BlobClient pdfBlob = inputContainer.GetBlobClient(blobItem.Name);
            using (MemoryStream pdfStream = new MemoryStream())
            {
                await pdfBlob.DownloadToAsync(pdfStream);
                pdfStream.Position = 0; // Reset for reading.

                // Extract text using Aspose.Pdf.Facades.PdfExtractor.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfStream);          // Bind the PDF stream.
                    extractor.ExtractText();               // Perform extraction.

                    // Retrieve extracted text into a string.
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);    // Save extracted text to stream.
                        string extractedText = Encoding.UTF8.GetString(textStream.ToArray());

                        // Prepare the output blob name (same as PDF but .txt extension).
                        string txtBlobName = Path.ChangeExtension(blobItem.Name, ".txt");
                        BlobClient txtBlob = outputContainer.GetBlobClient(txtBlobName);

                        // Upload the extracted text.
                        using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(extractedText)))
                        {
                            await txtBlob.UploadAsync(uploadStream, overwrite: true);
                        }

                        Console.WriteLine($"Uploaded extracted text to: {txtBlobName}");
                    }
                }
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}

// ---------------------------------------------------------------------------
// Minimal stub implementations for Azure.Storage.Blobs types.
// These allow the sample to compile without adding the real Azure SDK NuGet package.
// In production you should replace them with the official Azure.Storage.Blobs package.
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
        public Task CreateIfNotExistsAsync() => Task.CompletedTask;
        public BlobClient GetBlobClient(string blobName) => new BlobClient(blobName);
        public async IAsyncEnumerable<BlobItem> GetBlobsAsync([EnumeratorCancellation] System.Threading.CancellationToken cancellationToken = default)
        {
            // Stub: return an empty collection. Replace with real enumeration when using Azure SDK.
            await Task.Yield();
            yield break;
        }
    }

    public class BlobClient
    {
        private readonly string _blobName;
        public BlobClient(string blobName) => _blobName = blobName;
        public Task DownloadToAsync(Stream destination)
        {
            // Stub: write an empty PDF header so Aspose can bind without error.
            // In real usage the SDK streams the actual blob content.
            byte[] emptyPdf = Encoding.ASCII.GetBytes("%PDF-1.4\n%%EOF");
            return destination.WriteAsync(emptyPdf, 0, emptyPdf.Length);
        }
        public Task UploadAsync(Stream source, bool overwrite = false)
        {
            // Stub: simply consume the stream.
            return source.CopyToAsync(Stream.Null);
        }
    }
}

namespace Azure.Storage.Blobs.Models
{
    public class BlobItem
    {
        public string Name { get; set; }
        public BlobItem() { }
        public BlobItem(string name) => Name = name;
    }
}
