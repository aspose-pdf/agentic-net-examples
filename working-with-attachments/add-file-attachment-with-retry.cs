using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths on the network share
        const string inputPdfPath      = @"\\networkshare\docs\source.pdf";
        const string attachmentPath    = @"\\networkshare\files\attachment.docx";
        const string outputPdfPath     = @"\\networkshare\docs\source_with_attachment.pdf";

        // Retry configuration
        const int maxRetries = 5;
        const int delayMilliseconds = 2000; // 2 seconds between attempts

        int attempt = 0;
        bool success = false;

        while (attempt < maxRetries && !success)
        {
            try
            {
                // Load the PDF (using block ensures deterministic disposal)
                using (Document doc = new Document(inputPdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count == 0)
                        throw new InvalidOperationException("The PDF contains no pages.");

                    // Use the first page (1‑based indexing)
                    Page page = doc.Pages[1];

                    // Create a file specification for the attachment
                    FileSpecification fileSpec = new FileSpecification(attachmentPath);

                    // Define the annotation rectangle (left, bottom, right, top)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                    // Create the file attachment annotation and add it to the page
                    FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
                    page.Annotations.Add(attachment);

                    // Save the modified PDF back to the network share
                    doc.Save(outputPdfPath);
                }

                // If we reach this point, the operation succeeded
                success = true;
                Console.WriteLine("Attachment added and PDF saved successfully.");
            }
            catch (IOException ioEx)
            {
                // Network‑related I/O errors are common on shared drives
                attempt++;
                Console.Error.WriteLine($"I/O error on attempt {attempt}: {ioEx.Message}");
                if (attempt < maxRetries)
                {
                    Thread.Sleep(delayMilliseconds);
                }
            }
            catch (UnauthorizedAccessException uaEx)
            {
                // Permission issues may be transient (e.g., file lock)
                attempt++;
                Console.Error.WriteLine($"Access error on attempt {attempt}: {uaEx.Message}");
                if (attempt < maxRetries)
                {
                    Thread.Sleep(delayMilliseconds);
                }
            }
            catch (Exception ex)
            {
                // Non‑recoverable errors – abort retries
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }

        if (!success)
        {
            Console.Error.WriteLine("Failed to add attachment after multiple attempts.");
        }
    }
}