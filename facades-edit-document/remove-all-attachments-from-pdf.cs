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

        // PdfContentEditor implements IDisposable (via SaveableFacade), so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Remove all embedded attachments.
            editor.DeleteAttachments();

            // Save the resulting PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"All attachments removed. Saved to '{outputPath}'.");
    }
}