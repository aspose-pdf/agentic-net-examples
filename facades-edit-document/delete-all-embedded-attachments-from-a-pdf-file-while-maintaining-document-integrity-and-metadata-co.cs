using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfContentEditor instance to work with the PDF file
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Remove all embedded attachments
            editor.DeleteAttachments();

            // Save the cleaned PDF; existing metadata and document structure are preserved
            editor.Save(outputPath);
        }

        Console.WriteLine($"All attachments deleted. Saved to '{outputPath}'.");
    }
}