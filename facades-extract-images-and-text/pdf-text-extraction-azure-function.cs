using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs; // Added to bring stub attributes into scope
using Azure.Storage.Blobs;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Stub definitions for missing Azure Functions and Azure Storage SDK types.
// These allow the project to compile when the real NuGet packages are not
// referenced (e.g., during unit‑test compilation). In a production Azure
// Function app you should remove these stubs and add the proper package
// references:
//   • Microsoft.Azure.WebJobs
//   • Azure.Storage.Blobs
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
        public string? Connection { get; set; }
    }
}

namespace Azure.Storage.Blobs
{
    public class BlobServiceClient
    {
        private readonly string _connectionString;
        public BlobServiceClient(string connectionString) => _connectionString = connectionString;
        public BlobContainerClient GetBlobContainerClient(string containerName) => new BlobContainerClient();
    }

    public class BlobContainerClient
    {
        public BlobClient GetBlobClient(string blobName) => new BlobClient();
        public Task CreateIfNotExistsAsync() => Task.CompletedTask;
    }

    public class BlobClient
    {
        public Task<bool> ExistsAsync() => Task.FromResult(true);
        public Task DownloadToAsync(Stream destination) => Task.CompletedTask;
        public Task UploadAsync(Stream source, bool overwrite = false) => Task.CompletedTask;
    }
}

public static class PdfTextExtractionFunction
{
    // Queue message should contain the name of the PDF blob (e.g., "documents/sample.pdf")
    [FunctionName("PdfTextExtractionFunction")]
    public static async Task Run(
        [QueueTrigger("pdf-queue", Connection = "AzureWebJobsStorage")] string queueMessage,
        ILogger log)
    {
        if (string.IsNullOrWhiteSpace(queueMessage))
        {
            log.LogWarning("Queue message is empty. Skipping.");
            return;
        }

        // Configuration: adjust these values to match your storage account
        string blobConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage") ?? string.Empty;
        string sourceContainerName   = Environment.GetEnvironmentVariable("PdfSourceContainer")   ?? "pdf-source";
        string targetContainerName   = Environment.GetEnvironmentVariable("PdfTextContainer")    ?? "pdf-text";

        // Initialize Blob clients
        BlobServiceClient blobService = new BlobServiceClient(blobConnectionString);
        BlobContainerClient sourceContainer = blobService.GetBlobContainerClient(sourceContainerName);
        BlobContainerClient targetContainer = blobService.GetBlobContainerClient(targetContainerName);

        // Ensure target container exists
        await targetContainer.CreateIfNotExistsAsync();

        // Get reference to the PDF blob
        BlobClient pdfBlob = sourceContainer.GetBlobClient(queueMessage);
        if (!await pdfBlob.ExistsAsync())
        {
            log.LogError($"PDF blob '{queueMessage}' not found in container '{sourceContainerName}'.");
            return;
        }

        // Download PDF into a memory stream
        using (MemoryStream pdfStream = new MemoryStream())
        {
            await pdfBlob.DownloadToAsync(pdfStream);
            pdfStream.Position = 0; // reset for reading

            // Extract text using Aspose.Pdf.Facades.PdfExtractor
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfStream);
                extractor.ExtractText(); // default Unicode extraction

                // Store extracted text in a memory stream
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    textStream.Position = 0;

                    // Convert to string (UTF-8)
                    string extractedText = Encoding.UTF8.GetString(textStream.ToArray());

                    // Prepare target blob name (same as source but .txt extension)
                    string textBlobName = Path.ChangeExtension(queueMessage, ".txt");
                    BlobClient textBlob = targetContainer.GetBlobClient(textBlobName);

                    // Upload extracted text
                    using (MemoryStream uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(extractedText)))
                    {
                        await textBlob.UploadAsync(uploadStream, overwrite: true);
                    }

                    log.LogInformation($"Extracted text from '{queueMessage}' and uploaded as '{textBlobName}'.");
                }
            }
        }
    }
}

// Dummy entry point to satisfy the C# compiler when the project is built as a console app.
public class Program
{
    public static void Main(string[] args)
    {
        // Azure Functions are triggered by the runtime; no manual execution is required.
    }
}
