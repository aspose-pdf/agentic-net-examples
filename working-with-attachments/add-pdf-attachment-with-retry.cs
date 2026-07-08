using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfAttachmentHelper
{
    // Adds a file attachment annotation to the first page of the PDF.
    // Retries the operation up to maxRetries times if a transient I/O error occurs.
    public static void AddAttachmentWithRetry(string pdfPath, string fileToAttach, int maxRetries = 3, int delayMilliseconds = 1000)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF not found: {pdfPath}");

        if (!File.Exists(fileToAttach))
            throw new FileNotFoundException($"Attachment file not found: {fileToAttach}");

        int attempt = 0;
        while (true)
        {
            try
            {
                // Load the PDF (lifecycle rule: use Document constructor)
                using (Document doc = new Document(pdfPath))
                {
                    // Create a FileSpecification for the attachment
                    FileSpecification fileSpec = new FileSpecification(fileToAttach);

                    // Define the rectangle where the attachment icon will appear
                    // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

                    // Create the attachment annotation on the first page
                    Page page = doc.Pages[1]; // 1‑based indexing (global rule)
                    FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);

                    // Add the annotation to the page
                    page.Annotations.Add(attachment);

                    // Save the modified PDF (lifecycle rule: use Document.Save(string))
                    doc.Save(pdfPath);
                }

                // If we reach this point, the operation succeeded
                break;
            }
            catch (IOException ex) when (attempt < maxRetries)
            {
                // Transient I/O error – wait and retry
                attempt++;
                Console.Error.WriteLine($"I/O error on attempt {attempt}: {ex.Message}. Retrying in {delayMilliseconds} ms...");
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                // Non‑recoverable error or max retries exceeded
                Console.Error.WriteLine($"Failed to add attachment: {ex.Message}");
                throw;
            }
        }
    }

    // Example usage
    static void Main()
    {
        const string networkPdf = @"\\fileserver\shared\documents\sample.pdf";
        const string attachment = @"C:\Temp\info.txt";

        try
        {
            AddAttachmentWithRetry(networkPdf, attachment);
            Console.WriteLine("Attachment added successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Operation failed: {ex.Message}");
        }
    }
}