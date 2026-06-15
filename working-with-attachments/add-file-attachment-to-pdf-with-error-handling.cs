using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfAttachmentHelper
{
    // Adds a file attachment to the first page of a PDF.
    // Handles missing source PDF or attachment file gracefully.
    public static void AddAttachment(string pdfPath, string attachmentPath, string outputPath)
    {
        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'.");
            return;
        }

        // Verify that the attachment file exists.
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Error: Attachment file not found – '{attachmentPath}'.");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal).
            using (Document doc = new Document(pdfPath))
            {
                // Ensure the document has at least one page.
                if (doc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: PDF contains no pages.");
                    return;
                }

                // Create a FileSpecification for the attachment.
                FileSpecification fileSpec = new FileSpecification(attachmentPath);

                // Define a rectangle for the annotation (position on the page).
                // Fully qualified to avoid ambiguity.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                // Create the FileAttachment annotation on the first page.
                Page page = doc.Pages[1];
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional visual properties.
                    Icon = FileIcon.Paperclip, // Use the correct enum provided by Aspose.Pdf.
                    Contents = $"Attached file: {System.IO.Path.GetFileName(attachmentPath)}",
                    Title = "File Attachment"
                };

                // Add the annotation to the page.
                page.Annotations.Add(attachment);

                // Save the modified PDF (lifecycle rule: use Save inside using).
                doc.Save(outputPath);
                Console.WriteLine($"Attachment added successfully. Saved to '{outputPath}'.");
            }
        }
        catch (PdfException ex)
        {
            // Handles errors thrown by Aspose.Pdf during processing.
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General fallback for unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string fileToAttach = "attachment.txt";
        const string outputPdf = "output_with_attachment.pdf";

        AddAttachment(inputPdf, fileToAttach, outputPdf);
    }
}
