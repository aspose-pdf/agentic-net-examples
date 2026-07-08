using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath        = "input.pdf";          // PDF to which the attachment will be added
        const string attachmentPath = "document.txt";       // File to attach
        const string outputPath     = "output_with_attachment.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'.");
            return;
        }

        // Verify that the attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Error: Attachment file not found – '{attachmentPath}'.");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
            using (Document doc = new Document(pdfPath))
            {
                // Create a FileSpecification for the attachment
                FileSpecification fileSpec = new FileSpecification(attachmentPath);

                // Define the rectangle for the annotation (position and size on the page)
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

                // Add the attachment to the first page (pages are 1‑based)
                Page page = doc.Pages[1];
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional visual properties – use the supported FileIcon enum
                    Icon = FileIcon.Paperclip,
                    Contents = $"Attached file: {Path.GetFileName(attachmentPath)}",
                    Title = "File Attachment"
                };

                page.Annotations.Add(attachment);

                // Save the modified PDF (lifecycle rule: use Document.Save inside the using block)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment added successfully. Saved as '{outputPath}'.");
        }
        catch (PdfException pdfEx)
        {
            // Handles errors specific to Aspose.Pdf processing
            Console.Error.WriteLine($"PDF processing error: {pdfEx.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
