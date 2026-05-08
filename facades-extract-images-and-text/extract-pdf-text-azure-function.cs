using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Aspose.Pdf.Facades;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

// ---------- Stubs for missing Azure Functions and Storage SDK ----------
namespace Microsoft.Azure.WebJobs
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class FunctionNameAttribute : Attribute
    {
        public FunctionNameAttribute(string name) { }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class QueueTriggerAttribute : Attribute
    {
        public QueueTriggerAttribute(string queueName) { }
        public string Connection { get; set; }
    }
}

namespace Microsoft.Extensions.Logging
{
    public interface ILogger
    {
        void LogError(string message);
        void LogInformation(string message);
    }

    // Simple console logger used when the real logger is not available.
    public class ConsoleLogger : ILogger
    {
        public void LogError(string message) => Console.Error.WriteLine($"ERROR: {message}");
        public void LogInformation(string message) => Console.WriteLine($"INFO: {message}");
    }
}

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

        // In a real implementation these would talk to Azure Storage.
        // Here they are simple in‑memory / file‑system placeholders.
        public Task<bool> ExistsAsync() => Task.FromResult(File.Exists(GetLocalPath()));
        public Task DownloadToAsync(Stream destination)
        {
            var path = GetLocalPath();
            if (!File.Exists(path)) throw new FileNotFoundException($"Blob '{_blobName}' not found in container '{_containerName}'.");
            using var source = File.OpenRead(path);
            return source.CopyToAsync(destination);
        }
        public Task UploadAsync(Stream source, bool overwrite = false)
        {
            var path = GetLocalPath();
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            using var file = new FileStream(path, overwrite ? FileMode.Create : FileMode.CreateNew);
            return source.CopyToAsync(file);
        }
        private string GetLocalPath() => Path.Combine("LocalStorage", _containerName, _blobName);
    }
}

// ----------------------------------------------------------------------

public static class PdfQueueProcessor
{
    // Azure Function triggered by a message in the "pdfqueue" storage queue.
    // The message is expected to contain the name of the PDF blob to process.
    [FunctionName("PdfQueueProcessor")]
    public static async Task Run(
        [QueueTrigger("pdfqueue", Connection = "AzureWebJobsStorage")] string blobName,
        ILogger log)
    {
        const string pdfContainerName = "pdfcontainer";
        const string textContainerName = "textcontainer";

        // Connection string for Azure Storage (set in application settings).
        string storageConnection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        if (string.IsNullOrWhiteSpace(storageConnection))
        {
            log.LogError("AzureWebJobsStorage connection string is missing.");
            return;
        }

        // Initialize Blob service client.
        BlobServiceClient blobService = new BlobServiceClient(storageConnection);
        BlobContainerClient pdfContainer = blobService.GetBlobContainerClient(pdfContainerName);
        BlobContainerClient textContainer = blobService.GetBlobContainerClient(textContainerName);

        // Get reference to the PDF blob.
        BlobClient pdfBlob = pdfContainer.GetBlobClient(blobName);
        if (!await pdfBlob.ExistsAsync())
        {
            log.LogError($"PDF blob '{blobName}' not found in container '{pdfContainerName}'.");
            return;
        }

        // Download PDF content into a memory stream.
        using (MemoryStream pdfStream = new MemoryStream())
        {
            await pdfBlob.DownloadToAsync(pdfStream);
            pdfStream.Position = 0; // Reset stream position for reading.

            // Extract text using Aspose.Pdf.Facades.PdfExtractor.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF stream to the extractor.
                extractor.BindPdf(pdfStream);

                // Perform text extraction (Unicode encoding is default).
                extractor.ExtractText();

                // Store extracted text into a memory stream.
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    textStream.Position = 0; // Reset for upload.

                    // Prepare the output blob name (same as input but with .txt extension).
                    string textBlobName = Path.ChangeExtension(blobName, ".txt");
                    BlobClient textBlob = textContainer.GetBlobClient(textBlobName);

                    // Upload the extracted text.
                    await textBlob.UploadAsync(textStream, overwrite: true);
                    log.LogInformation($"Extracted text from '{blobName}' and uploaded as '{textBlobName}'.");
                }
            }
        }
    }

    // ------------------------------------------------------------------
    // Helper Main method to allow local testing without the Azure Functions runtime.
    // This is optional and can be removed when the function is deployed.
    // ------------------------------------------------------------------
    public static async Task Main(string[] args)
    {
        // Use the console logger when running locally.
        ILogger logger = new Microsoft.Extensions.Logging.ConsoleLogger();
        // Expect a single argument: the blob name to process.
        if (args.Length == 0)
        {
            logger.LogError("Please provide the PDF blob name as the first argument.");
            return;
        }
        await Run(args[0], logger);
    }
}
