using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure Functions attributes (Microsoft.Azure.WebJobs)
// ---------------------------------------------------------------------------
namespace Microsoft.Azure.WebJobs
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class FunctionNameAttribute : Attribute
    {
        public FunctionNameAttribute(string name) { }
    }

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class QueueTriggerAttribute : Attribute
    {
        public QueueTriggerAttribute(string queueName) { }
        // Provide a non‑null default to satisfy the non‑nullable warning
        public string Connection { get; set; } = string.Empty;
    }
}

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs SDK – only the members used in the code
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
        public async Task CreateIfNotExistsAsync() => await Task.CompletedTask; // No‑op stub
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

        // Stub that always pretends the blob exists – replace with real logic in production
        public async Task<bool> ExistsAsync() => await Task.FromResult(true);

        // Stub download – writes an empty PDF header so Aspose can open it without error
        public async Task DownloadToAsync(Stream destination)
        {
            // Minimal PDF header ("%PDF-1.1") – enough for Aspose to treat it as a PDF
            var header = Encoding.ASCII.GetBytes("%PDF-1.1\n%âãÏÓ\n");
            await destination.WriteAsync(header, 0, header.Length);
            await destination.FlushAsync();
        }

        // Stub upload – simply discards the stream
        public async Task UploadAsync(Stream source, bool overwrite = false) => await Task.CompletedTask;
    }
}

// ---------------------------------------------------------------------------
// Azure Function that extracts text from PDFs placed on a storage queue
// ---------------------------------------------------------------------------
public static class PdfQueueProcessor
{
    // Azure Function triggered by a Storage Queue named "pdfqueue"
    [Microsoft.Azure.WebJobs.FunctionName("PdfQueueProcessor")]
    public static async Task Run(
        [Microsoft.Azure.WebJobs.QueueTrigger("pdfqueue", Connection = "AzureWebJobsStorage")] string queueMessage,
        ILogger log)
    {
        // queueMessage is expected to be the name of the PDF blob in the "pdf-files" container
        const string sourceContainerName = "pdf-files";
        const string destinationContainerName = "extracted-text";

        // Retrieve storage connection string from the same setting used by the function runtime
        string storageConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        if (string.IsNullOrWhiteSpace(storageConnectionString))
        {
            log.LogError("AzureWebJobsStorage connection string is missing.");
            return;
        }

        // Initialize Blob service client (stub implementation above)
        var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(storageConnectionString);
        var sourceContainer = blobServiceClient.GetBlobContainerClient(sourceContainerName);
        var destinationContainer = blobServiceClient.GetBlobContainerClient(destinationContainerName);

        // Ensure destination container exists
        await destinationContainer.CreateIfNotExistsAsync();

        // Get reference to the source PDF blob
        var sourceBlob = sourceContainer.GetBlobClient(queueMessage);
        if (!await sourceBlob.ExistsAsync())
        {
            log.LogError($"Blob '{queueMessage}' not found in container '{sourceContainerName}'.");
            return;
        }

        // Download PDF into a memory stream
        using (var pdfStream = new MemoryStream())
        {
            await sourceBlob.DownloadToAsync(pdfStream);
            pdfStream.Position = 0; // reset for reading

            // Use Aspose.Pdf.Facades.PdfExtractor to extract text
            using (var extractor = new PdfExtractor())
            {
                // Bind the PDF stream to the extractor
                extractor.BindPdf(pdfStream);

                // Extract text using Unicode encoding (default is fine, but explicit for clarity)
                extractor.ExtractText();

                // Retrieve extracted text into another memory stream
                using (var textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    string extractedText = Encoding.UTF8.GetString(textStream.ToArray());

                    // Log a preview of the extracted content
                    log.LogInformation($"Extracted text from '{queueMessage}': {ExtractPreview(extractedText)}");

                    // Upload the extracted text as a .txt file to the destination container
                    string textBlobName = Path.ChangeExtension(queueMessage, ".txt");
                    var destinationBlob = destinationContainer.GetBlobClient(textBlobName);

                    // Reset stream position before upload
                    textStream.Position = 0;
                    await destinationBlob.UploadAsync(textStream, overwrite: true);
                }
            }
        }
    }

    // Helper to create a short preview for logging purposes
    private static string ExtractPreview(string text, int maxLength = 200)
    {
        if (string.IsNullOrEmpty(text))
            return "(empty)";
        return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
    }
}

// ---------------------------------------------------------------------------
// Dummy entry point to satisfy the compiler when the project is built as an
// executable. Azure Functions are discovered via reflection, so the Main method
// does not need to perform any work.
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main(string[] args)
    {
        // No‑op entry point – Azure Functions runtime will invoke the methods
        // marked with [FunctionName] attributes.
    }
}
