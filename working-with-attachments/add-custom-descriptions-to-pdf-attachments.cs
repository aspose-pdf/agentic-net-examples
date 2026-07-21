using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the files to be attached.
        const string sourcePdfPath = "input.pdf";
        const string attachmentPath1 = "attachment1.txt";
        const string attachmentPath2 = "image.png";
        const string outputPdfPath = "output_with_attachments.pdf";

        // Verify that all required files exist.
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath1))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath1}");
            return;
        }
        if (!File.Exists(attachmentPath2))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath2}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(sourcePdfPath))
        {
            // Create the first file specification with a custom description.
            FileSpecification fileSpec1 = new FileSpecification(
                attachmentPath1,
                "Text file containing notes"); // description

            // Create the second file specification with its own description.
            FileSpecification fileSpec2 = new FileSpecification(
                attachmentPath2,
                "Sample image attachment"); // description

            // Add the file specifications to the document's embedded files collection.
            pdfDoc.EmbeddedFiles.Add(fileSpec1);
            pdfDoc.EmbeddedFiles.Add(fileSpec2);

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdfPath}");
    }
}
