using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure SDK and Azure Functions attributes (used when the
// real NuGet packages are not referenced). These are only needed to make the
// project compile for the purpose of this example.
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
        public Task DownloadToAsync(Stream destination) => Task.CompletedTask;
        public Task UploadAsync(Stream source, bool overwrite = false) => Task.CompletedTask;
    }
}

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
    // NOTE: Aspose.Pdf also defines a type named ILogger. To avoid the CS0436
    // conflict we keep this stub but always reference it with its fully
    // qualified name (Microsoft.Extensions.Logging.ILogger).
    public interface ILogger
    {
        void LogInformation(string message);
    }

    // Simple console logger implementation – sufficient for the sample.
    public class ConsoleLogger : ILogger
    {
        public void LogInformation(string message) => Console.WriteLine(message);
    }
}

public static class PdfQueueProcessor
{
    private static readonly string BlobConnectionString = Environment.GetEnvironmentVariable("BlobConnectionString");
    private static readonly string OutputContainerName = Environment.GetEnvironmentVariable("OutputContainerName");

    [FunctionName("ProcessPdfQueue")]
    public static async Task RunAsync(
        [QueueTrigger("pdf-queue", Connection = "QueueConnectionString")] string queueMessage,
        Microsoft.Extensions.Logging.ILogger logger) // fully‑qualified to avoid ambiguity
    {
        // The queue message is expected to contain the name of the PDF blob.
        string blobName = queueMessage;
        logger.LogInformation($"Processing PDF blob: {blobName}");

        var blobService = new BlobServiceClient(BlobConnectionString);
        var inputContainer = blobService.GetBlobContainerClient("pdf-input");
        var inputBlob = inputContainer.GetBlobClient(blobName);

        using (var pdfStream = new MemoryStream())
        {
            await inputBlob.DownloadToAsync(pdfStream);
            pdfStream.Position = 0;

            using (var pdfDocument = new Document(pdfStream))
            {
                var absorber = new TextAbsorber();
                pdfDocument.Pages.Accept(absorber);
                string extractedText = absorber.Text;

                var outputContainer = blobService.GetBlobContainerClient(OutputContainerName);
                var outputBlob = outputContainer.GetBlobClient(Path.ChangeExtension(blobName, ".txt"));
                using (var textStream = new MemoryStream(Encoding.UTF8.GetBytes(extractedText)))
                {
                    await outputBlob.UploadAsync(textStream, overwrite: true);
                }

                logger.LogInformation($"Extracted text saved for {blobName}");
            }
        }
    }
}

// Azure Functions projects are class‑library projects and do not require a
// Main entry point. However, when the project is compiled as a console app the
// compiler expects a static Main method. Adding a minimal stub satisfies the
// compiler without affecting the function runtime.
public class Program
{
    public static void Main(string[] args)
    {
        // No‑op – the Azure Functions host will invoke the function methods.
    }
}
