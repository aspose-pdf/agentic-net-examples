using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;

class Program
{
    // Configuration for retry logic
    private const int MaxRetryAttempts = 5;          // maximum number of attempts
    private const int RetryDelayMilliseconds = 2000; // wait time between attempts

    static void Main()
    {
        const string networkPdfPath = @"\\fileserver\share\documents\report.pdf";
        const string attachmentPath = @"C:\Temp\attachment.txt";
        const string attachmentName = "Attachment.txt";

        if (!File.Exists(networkPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {networkPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        int attempt = 0;
        bool success = false;

        while (attempt < MaxRetryAttempts && !success)
        {
            attempt++;
            try
            {
                // Open the PDF from the network share inside a using block (rule: document-disposal-with-using)
                using (Document pdfDoc = new Document(networkPdfPath))
                {
                    // Create a FileSpecification for the attachment
                    var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
                    // Load the file bytes into a MemoryStream and assign to Contents
                    fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
                    // Optionally set a description or MIME type
                    fileSpec.Description = attachmentName;

                    // Add the file specification to the EmbeddedFiles collection
                    pdfDoc.EmbeddedFiles.Add(fileSpec);

                    // Save back to the same network location
                    pdfDoc.Save(networkPdfPath);
                }

                // If we reach this point, the operation succeeded
                success = true;
                Console.WriteLine($"Attachment added successfully on attempt {attempt}.");
            }
            catch (IOException ioEx)
            {
                // Network-related I/O errors are common on UNC paths; retry after a delay
                Console.Error.WriteLine($"I/O error on attempt {attempt}: {ioEx.Message}");
                if (attempt < MaxRetryAttempts)
                {
                    Console.WriteLine($"Waiting {RetryDelayMilliseconds} ms before retrying...");
                    Thread.Sleep(RetryDelayMilliseconds);
                }
                else
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Operation failed.");
                }
            }
            catch (Exception ex)
            {
                // Non-recoverable errors – abort immediately
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }
    }
}
