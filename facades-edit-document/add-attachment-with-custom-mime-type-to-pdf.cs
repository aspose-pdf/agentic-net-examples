using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string attachmentPath = "attachment.dat";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify that the source files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(sourcePdfPath))
        {
            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentPath);

            // Set a custom MIME type for the attached file
            fileSpec.MIMEType = "application/vnd.custom";

            // Optionally set a description (visible in PDF viewers)
            fileSpec.Description = "Custom attachment with MIME type";

            // Add the file specification to the document's embedded files collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added with custom MIME type. Saved to '{outputPdfPath}'.");
    }
}