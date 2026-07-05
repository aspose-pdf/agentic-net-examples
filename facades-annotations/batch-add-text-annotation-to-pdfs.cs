using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

// ---------------------------------------------------------------------------
// Minimal Azure Blob Storage stubs – only the members used by the sample code.
// In a real project add the NuGet package "Azure.Storage.Blobs" instead.
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
        public BlobClient GetBlobClient(string blobName) => new BlobClient(blobName);
        public Task CreateIfNotExistsAsync() => Task.CompletedTask;
        public async IAsyncEnumerable<BlobItem> GetBlobsAsync()
        {
            // Stub – no blobs are returned. Replace with real enumeration when using the real SDK.
            await Task.CompletedTask;
            yield break;
        }
    }

    public class BlobClient
    {
        private readonly string _blobName;
        public BlobClient(string blobName) => _blobName = blobName;
        public Task<Stream> OpenReadAsync() => Task.FromResult<Stream>(new MemoryStream());
        public Task UploadAsync(Stream content, bool overwrite = false) => Task.CompletedTask;
    }
}

namespace Azure.Storage.Blobs.Models
{
    public class BlobItem
    {
        public string Name { get; set; }
    }
}

class BatchPdfAnnotationProcessor
{
    // Entry point
    static async Task Main()
    {
        // Azure Blob Storage connection string and container names
        const string connectionString = "YourAzureBlobStorageConnectionString";
        const string inputContainerName = "input-pdf-container";
        const string outputContainerName = "output-pdf-container";

        // Create Blob service and container clients
        BlobServiceClient serviceClient = new BlobServiceClient(connectionString);
        BlobContainerClient inputContainer = serviceClient.GetBlobContainerClient(inputContainerName);
        BlobContainerClient outputContainer = serviceClient.GetBlobContainerClient(outputContainerName);

        // Ensure the output container exists
        await outputContainer.CreateIfNotExistsAsync();

        // Process each PDF blob in the input container
        await foreach (BlobItem blobItem in inputContainer.GetBlobsAsync())
        {
            // Only process .pdf files
            if (!blobItem.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                continue;

            Console.WriteLine($"Processing blob: {blobItem.Name}");

            // Open the source PDF as a read‑only stream
            BlobClient sourceBlob = inputContainer.GetBlobClient(blobItem.Name);
            using (Stream sourceStream = await sourceBlob.OpenReadAsync())
            {
                // Initialize PdfAnnotationEditor and bind the PDF stream
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(sourceStream); // BindPdf(Stream)

                    // Example operation: add a simple text annotation to the first page
                    // Fully qualify types to avoid ambiguity
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                    TextAnnotation textAnn = new TextAnnotation(editor.Document.Pages[1], rect)
                    {
                        Title = "Batch Note",
                        Contents = "Processed by batch job",
                        Color = Aspose.Pdf.Color.Yellow,
                        Open = true,
                        Icon = TextIcon.Note
                    };
                    editor.Document.Pages[1].Annotations.Add(textAnn);

                    // Save the modified PDF into a memory stream
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        editor.Save(outputStream); // Save to stream
                        outputStream.Position = 0; // Reset for upload

                        // Upload the modified PDF to the output container (overwrite if exists)
                        BlobClient destBlob = outputContainer.GetBlobClient(blobItem.Name);
                        await destBlob.UploadAsync(outputStream, overwrite: true);
                    }
                }
            }

            Console.WriteLine($"Finished processing blob: {blobItem.Name}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
