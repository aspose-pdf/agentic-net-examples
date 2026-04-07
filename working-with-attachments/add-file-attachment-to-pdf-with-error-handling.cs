using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AddAttachmentExample
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "output_with_attachment.pdf";

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        // Verify attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Error: Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // Load the PDF document (using rule for document disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Choose the page to place the attachment (first page)
                Page page = doc.Pages[1];

                // Define the rectangle for the annotation (position and size)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

                // Create a file specification for the attachment
                FileSpecification fileSpec = new FileSpecification(attachmentPath);

                // Create the file attachment annotation
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional: set a title and description
                    Title = "Attached File",
                    Contents = "See attached document."
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);

                // Save the modified PDF (using rule for saving)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment added successfully. Saved to '{outputPath}'.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}