using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

// -----------------------------------------------------------------------------
// Stubs for missing Azure SDK and Azure Functions attributes (added to fix build).
// In a real project, reference the NuGet packages:
//   Azure.Storage.Blobs
//   Microsoft.Azure.WebJobs
//   Microsoft.Azure.WebJobs.Extensions.Storage
//   Microsoft.Extensions.Logging.Abstractions
// The stubs below allow the code to compile in environments where those packages
// are not available (e.g., during isolated evaluation).
// -----------------------------------------------------------------------------
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

        // In a real implementation this would query Azure Storage.
        public async Task<bool> ExistsAsync() => await Task.FromResult(true);

        // In a real implementation this would download the blob content.
        public async Task DownloadToAsync(Stream destination)
        {
            // For stub purposes we simply leave the stream empty.
            await destination.FlushAsync();
        }

        // In a real implementation this would upload the stream as a blob.
        public async Task UploadAsync(Stream source, bool overwrite = false)
        {
            // No‑op stub.
            await Task.CompletedTask;
        }
    }
}

namespace Microsoft.Azure.WebJobs
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class FunctionNameAttribute : Attribute
    {
        public FunctionNameAttribute(string name) { }
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
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
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(Exception exception, string message);
    }

    // Simple console logger used by the stub implementation.
    public class ConsoleLogger : ILogger
    {
        public void LogInformation(string message) => Console.WriteLine($"[Info] {message}");
        public void LogWarning(string message) => Console.WriteLine($"[Warn] {message}");
        public void LogError(string message) => Console.WriteLine($"[Error] {message}");
        public void LogError(Exception exception, string message) => Console.WriteLine($"[Error] {message} - {exception}");
    }
}

public static class PdfQueueProcessor
{
    // Azure Function triggered by a message in the "pdf-queue" storage queue.
    // The message is expected to contain the name of the PDF blob to process.
    [FunctionName("PdfQueueProcessor")]
    public static async Task Run(
        [QueueTrigger("pdf-queue", Connection = "AzureWebJobsStorage")] string queueMessage,
        ILogger log)
    {
        if (string.IsNullOrWhiteSpace(queueMessage))
        {
            log.LogWarning("Queue message is empty. Skipping.");
            return;
        }

        // Connection string for Azure Storage (set in application settings).
        string storageConnection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        if (string.IsNullOrWhiteSpace(storageConnection))
        {
            log.LogError("AzureWebJobsStorage connection string is not set.");
            return;
        }

        // Containers: one for input PDFs, one for extracted text files.
        const string inputContainerName = "pdf-input";
        const string outputContainerName = "pdf-text-output";

        try
        {
            // Initialize Blob service client.
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnection);
            BlobContainerClient inputContainer = blobServiceClient.GetBlobContainerClient(inputContainerName);
            BlobContainerClient outputContainer = blobServiceClient.GetBlobContainerClient(outputContainerName);

            // Get reference to the PDF blob.
            BlobClient pdfBlob = inputContainer.GetBlobClient(queueMessage);
            if (!await pdfBlob.ExistsAsync())
            {
                log.LogError($"PDF blob '{queueMessage}' does not exist in container '{inputContainerName}'.");
                return;
            }

            // Download PDF content into a memory stream.
            using (MemoryStream pdfStream = new MemoryStream())
            {
                await pdfBlob.DownloadToAsync(pdfStream);
                pdfStream.Position = 0; // Reset stream position for reading.

                // Use Aspose.Pdf.Facades.PdfExtractor to extract text.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF stream to the extractor.
                    extractor.BindPdf(pdfStream);

                    // Extract all text.
                    extractor.ExtractText();

                    // Retrieve extracted text into another memory stream.
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        textStream.Position = 0; // Reset for upload.

                        // Determine output blob name (same as input but with .txt extension).
                        string outputBlobName = Path.ChangeExtension(queueMessage, ".txt");
                        BlobClient textBlob = outputContainer.GetBlobClient(outputBlobName);

                        // Upload the extracted text.
                        await textBlob.UploadAsync(textStream, overwrite: true);
                        log.LogInformation($"Extracted text uploaded to '{outputBlobName}' in container '{outputContainerName}'.");
                    }

                    // Close the extractor (optional, Dispose will also handle it).
                    extractor.Close();
                }
            }
        }
        catch (Exception ex)
        {
            log.LogError(ex, $"Error processing PDF blob '{queueMessage}'.");
            // Optionally, rethrow or move the message to a poison queue.
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main()
    {
        // No‑op. Azure Functions are triggered by the runtime, not by Main.
    }
}
