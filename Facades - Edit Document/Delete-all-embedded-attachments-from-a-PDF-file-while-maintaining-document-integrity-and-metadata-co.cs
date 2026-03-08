using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor provides DeleteAttachments() to purge all embedded files
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Remove every attachment while keeping metadata and structure intact
            editor.DeleteAttachments();

            // Persist the cleaned document
            editor.Save(outputPath);
        }

        Console.WriteLine($"All attachments removed. Saved to '{outputPath}'.");
    }
}