using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF (lifecycle: using block ensures disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Use the first page for the attachment annotation
                Page page = doc.Pages[1];

                // Define the rectangle for the attachment icon (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

                // Check that the file to be attached exists
                if (!File.Exists(attachmentPath))
                {
                    Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
                }
                else
                {
                    // Create a file specification for the attachment
                    FileSpecification fileSpec = new FileSpecification(attachmentPath);

                    // Create the file attachment annotation
                    FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                    {
                        // Use the correct enum for the icon
                        Icon = FileIcon.Paperclip,
                        Contents = "Attached file",
                        Title = "Attachment"
                    };

                    // Add the annotation to the page
                    page.Annotations.Add(attachment);
                }

                // Save the modified PDF (lifecycle: save inside using block)
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
        }
        catch (FileNotFoundException ex)
        {
            // Handles missing files for both PDF and attachment
            Console.Error.WriteLine($"File not found: {ex.FileName}");
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf operations
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch‑all for any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
