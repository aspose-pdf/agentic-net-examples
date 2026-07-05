using System;
using System.IO;
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

        // Use PdfPageEditor facade to edit page properties
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Apply changes only to page 2 (page numbers are 1‑based)
            editor.ProcessPages = new int[] { 2 };

            // Set transition type to BoxOut (OUTBOX constant)
            editor.TransitionType = PdfPageEditor.OUTBOX;

            // Set zoom factor to 1.3 (130%). Use a float literal.
            editor.Zoom = 1.3f;

            // Apply the configured changes
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
