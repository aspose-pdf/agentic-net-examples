using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfAttachmentHelper
{
    // Adds a file attachment to a PDF with retry logic for network share access issues.
    public static void AddAttachmentWithRetry(string pdfPath, string attachmentFilePath, string outputPath, int maxRetries = 3, int delayMilliseconds = 1000)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF not found: {pdfPath}");
        if (!File.Exists(attachmentFilePath))
            throw new FileNotFoundException($"Attachment not found: {attachmentFilePath}");

        int attempt = 0;
        while (true)
        {
            try
            {
                // Load the PDF document (lifecycle rule: use Document constructor)
                using (Document doc = new Document(pdfPath))
                {
                    // Create a file specification for the attachment
                    FileSpecification fileSpec = new FileSpecification(attachmentFilePath);

                    // Define the rectangle where the attachment icon will appear (fully qualified to avoid ambiguity)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 120, 120);

                    // Add the attachment annotation to the first page
                    Page page = doc.Pages[1];
                    FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
                    // Use the correct enum (FileIcon) for the icon
                    attachment.Icon = FileIcon.PushPin; // optional visual style
                    page.Annotations.Add(attachment);

                    // Save the modified PDF (lifecycle rule: use Document.Save(string))
                    doc.Save(outputPath);
                }

                // If we reach here, the operation succeeded
                break;
            }
            catch (IOException ex) when (attempt < maxRetries)
            {
                // Network-related I/O errors are retried
                attempt++;
                Console.Error.WriteLine($"I/O error on attempt {attempt}: {ex.Message}. Retrying in {delayMilliseconds} ms...");
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                // Non-retriable errors or max retries exceeded
                Console.Error.WriteLine($"Failed to add attachment: {ex.Message}");
                throw;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Use a temporary local folder for the demo (avoids missing network share paths)
        string baseDir = Path.Combine(Path.GetTempPath(), "AsposeDemo");
        Directory.CreateDirectory(baseDir);

        string pdfPath = Path.Combine(baseDir, "sample.pdf");
        string attachmentPath = Path.Combine(baseDir, "info.txt");
        string outputPath = Path.Combine(baseDir, "sample_with_attachment.pdf");

        // Ensure a source PDF exists (seed it if it does not)
        if (!File.Exists(pdfPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(pdfPath);
            }
        }

        // Ensure an attachment file exists
        if (!File.Exists(attachmentPath))
        {
            File.WriteAllText(attachmentPath, "Sample attachment content.");
        }

        // Perform the attachment with retry handling
        PdfAttachmentHelper.AddAttachmentWithRetry(pdfPath, attachmentPath, outputPath);

        Console.WriteLine($"Attachment added successfully. Output saved to: {outputPath}");
    }
}
