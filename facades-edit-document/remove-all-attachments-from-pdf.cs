using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Remove all embedded file attachments, if any
        if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
        {
            // EmbeddedFileCollection does not support Clear(); use Delete() to remove all attachments
            pdfDoc.EmbeddedFiles.Delete();
        }

        // Save the modified PDF without attachments
        pdfDoc.Save(outputPath);

        Console.WriteLine($"All attachments removed. Saved to '{outputPath}'.");
    }
}
