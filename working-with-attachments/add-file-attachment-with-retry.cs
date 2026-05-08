using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfAttachmentHelper
{
    // Adds a file attachment to the first page of a PDF.
    // Retries the whole operation if a transient I/O error occurs.
    public static void AddAttachmentWithRetry(
        string pdfPath,
        string attachmentFilePath,
        string outputPath,
        int maxRetries = 3,
        int initialDelayMs = 500)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF not found: {pdfPath}");

        if (!File.Exists(attachmentFilePath))
            throw new FileNotFoundException($"Attachment not found: {attachmentFilePath}");

        int attempt = 0;
        int delay = initialDelayMs;

        while (true)
        {
            try
            {
                // Load the PDF (lifecycle rule: use Document constructor)
                using (Document doc = new Document(pdfPath))
                {
                    // Create a file specification for the attachment
                    FileSpecification fileSpec = new FileSpecification(attachmentFilePath);

                    // Define the rectangle where the attachment icon will appear
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 200, 200);

                    // Get the first page (Aspose.Pdf uses 1‑based indexing)
                    Page page = doc.Pages[1];

                    // Create the attachment annotation and add it to the page
                    FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
                    page.Annotations.Add(attachment);

                    // Save the modified PDF (lifecycle rule: use Document.Save)
                    doc.Save(outputPath);
                }

                // If we reach this point the operation succeeded
                break;
            }
            catch (IOException ex) when (attempt < maxRetries)
            {
                // Transient I/O error – wait and retry
                attempt++;
                Console.Error.WriteLine($"I/O error on attempt {attempt}: {ex.Message}. Retrying in {delay} ms...");
                Thread.Sleep(delay);
                delay *= 2; // exponential back‑off
            }
            catch (UnauthorizedAccessException ex) when (attempt < maxRetries)
            {
                // Access issue – also retry (e.g., temporary lock on network share)
                attempt++;
                Console.Error.WriteLine($"Access error on attempt {attempt}: {ex.Message}. Retrying in {delay} ms...");
                Thread.Sleep(delay);
                delay *= 2;
            }
            catch
            {
                // Non‑recoverable exception – rethrow
                throw;
            }
        }
    }
}

// Example usage
class Program
{
    static void Main()
    {
        const string networkPdf = @"\\fileserver\share\documents\sample.pdf";
        const string attachment = @"C:\Temp\info.txt";
        const string outputPdf = @"\\fileserver\share\documents\sample_with_attachment.pdf";

        try
        {
            PdfAttachmentHelper.AddAttachmentWithRetry(networkPdf, attachment, outputPdf);
            Console.WriteLine("Attachment added successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to add attachment: {ex.Message}");
        }
    }
}