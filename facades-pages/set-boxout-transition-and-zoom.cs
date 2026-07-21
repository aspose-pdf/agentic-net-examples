using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Apply changes only to page 2 (1‑based indexing)
            editor.ProcessPages = new int[] { 2 };

            // Set transition type to BoxOut (outward box)
            editor.TransitionType = PdfPageEditor.OUTBOX;

            // Set zoom factor to 1.3 (130%) – float literal required
            editor.Zoom = 1.3f;

            // Optional: set transition duration (in seconds)
            editor.TransitionDuration = 2;

            // Apply the configured changes
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
