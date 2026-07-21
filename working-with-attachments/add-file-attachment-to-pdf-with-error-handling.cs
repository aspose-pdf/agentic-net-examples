using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF to which the attachment will be added
        const string attachmentPath = "document.txt"; // File to attach
        const string outputPath = "output_with_attachment.pdf";

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
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Choose the page where the annotation will be placed (first page in this example)
                Page page = doc.Pages[1];

                // Define the rectangle for the attachment annotation
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

                // Create a FileSpecification for the attachment
                // The stream is opened in a using block to ensure it is closed promptly
                using (FileStream fs = new FileStream(attachmentPath, FileMode.Open, FileAccess.Read))
                {
                    FileSpecification fileSpec = new FileSpecification(fs, Path.GetFileName(attachmentPath));

                    // Create the FileAttachment annotation
                    FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                    {
                        // Optional properties
                        Title = "Attached Document",
                        Contents = "See attached file.",
                        // Set a visual cue for the annotation (border color)
                        Color = Aspose.Pdf.Color.Blue
                        // Icon property omitted because the enum may not be available in all SDK versions
                    };

                    // Add the annotation to the page
                    page.Annotations.Add(attachment);
                }

                // Save the modified PDF (lifecycle rule: use Save without extra options for PDF output)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment added successfully. Saved as '{outputPath}'.");
        }
        catch (PdfException ex)
        {
            // Handles errors thrown by Aspose.Pdf (e.g., corrupted PDF)
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General fallback for unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
