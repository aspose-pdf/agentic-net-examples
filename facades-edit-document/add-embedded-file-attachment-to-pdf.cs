using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "input.pdf";
        const string attachmentPath  = "attachment.txt";
        const string outputPdfPath   = "output_with_attachment.pdf";

        // Verify that source files exist
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

        // Load the original PDF (Document class is the primary API for PDF manipulation)
        Document pdfDoc = new Document(sourcePdfPath);

        // Create a file specification for the attachment using the correct constructor
        FileSpecification fileSpec = new FileSpecification(attachmentPath, "Sample attachment description");
        // Optional: set a friendly display name (auto‑derived from the file name if not set)
        fileSpec.Name = Path.GetFileName(attachmentPath);
        // Optional: set a description (already supplied via constructor, but can be overridden)
        fileSpec.Description = "Sample attachment description";

        // Add the file specification to the EmbeddedFiles name tree – this defines the relationship as an embedded file
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF to a new file so the original remains unchanged
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"Attachment added successfully to '{outputPdfPath}'.");
    }
}
