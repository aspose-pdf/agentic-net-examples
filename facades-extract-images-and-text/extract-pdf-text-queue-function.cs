using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Minimal stubs for Azure Functions attributes when the Microsoft.Azure.WebJobs
// package is not referenced. These definitions are sufficient for compilation
// and unit‑testing of the function logic.
// -----------------------------------------------------------------------------
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
        // Made nullable to satisfy the non‑nullable reference type analysis.
        public string? Connection { get; set; }
    }
}

// -----------------------------------------------------------------------------
// Minimal Azure.Storage.Blobs stubs – used when the real Azure.Storage.Blobs
// NuGet package is not available. They provide just enough surface area for the
// sample function to compile and run in a test environment.
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
        public Task CreateIfNotExistsAsync() => Task.CompletedTask;
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
        public Task<bool> ExistsAsync() => Task.FromResult(true);
        public Task DownloadToAsync(Stream destination)
        {
            // In a real implementation the blob would be downloaded.
            // For the stub we simply complete the task.
            return Task.CompletedTask;
        }
        public Task UploadAsync(Stream source, bool overwrite = false)
        {
            // In a real implementation the blob would be uploaded.
            // For the stub we simply complete the task.
            return Task.CompletedTask;
        }
    }
}

public static class PdfQueueProcessor
{
    // This function is triggered by a message on the "pdf-queue" Azure Storage Queue.
    // The message is expected to contain the name of a PDF blob stored in the "pdf-files" container.
    [Microsoft.Azure.WebJobs.FunctionName("PdfQueueProcessor")]
    public static async Task Run(
        [Microsoft.Azure.WebJobs.QueueTrigger("pdf-queue", Connection = "AzureWebJobsStorage")] string queueMessage,
        ILogger log)
    {
        if (string.IsNullOrWhiteSpace(queueMessage))
        {
            log.LogWarning("Queue message is empty. Skipping.");
            return;
        }

        // Connection string for Azure Storage (set in application settings).
        string storageConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        if (string.IsNullOrWhiteSpace(storageConnectionString))
        {
            log.LogError("AzureWebJobsStorage is not set.");
            return;
        }

        // Blob containers: one for input PDFs, one for extracted text files.
        const string inputContainerName = "pdf-files";
        const string outputContainerName = "pdf-text";

        // Initialize Blob service client.
        BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
        BlobContainerClient inputContainer = blobServiceClient.GetBlobContainerClient(inputContainerName);
        BlobContainerClient outputContainer = blobServiceClient.GetBlobContainerClient(outputContainerName);

        // Ensure the output container exists.
        await outputContainer.CreateIfNotExistsAsync();

        // Reference to the PDF blob.
        BlobClient pdfBlob = inputContainer.GetBlobClient(queueMessage);
        if (!await pdfBlob.ExistsAsync())
        {
            log.LogError($"PDF blob '{queueMessage}' not found in container '{inputContainerName}'.");
            return;
        }

        // Download PDF into a memory stream.
        using (MemoryStream pdfStream = new MemoryStream())
        {
            await pdfBlob.DownloadToAsync(pdfStream);
            pdfStream.Position = 0; // Reset stream position for reading.

            // Use Aspose.Pdf.Facades.PdfExtractor to extract text.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfStream);
                extractor.ExtractText(); // Extract using default Unicode encoding.

                // Capture extracted text into a memory stream.
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    string extractedText = Encoding.UTF8.GetString(textStream.ToArray());

                    // Prepare output blob name (same as PDF but with .txt extension).
                    string textBlobName = Path.GetFileNameWithoutExtension(queueMessage) + ".txt";
                    BlobClient textBlob = outputContainer.GetBlobClient(textBlobName);

                    // Upload extracted text.
                    using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(extractedText)))
                    {
                        await textBlob.UploadAsync(uploadStream, overwrite: true);
                    }

                    log.LogInformation($"Extracted text from '{queueMessage}' and saved to '{textBlobName}'.");
                }
            }
        }
    }
}

// -----------------------------------------------------------------------------
// Dummy entry point to satisfy the compiler when the project is built as an
// executable. Azure Functions are discovered via reflection, so the Main method
// does not need to perform any work.
// -----------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // No-op – the Azure Functions runtime will invoke the function methods.
    }
}
