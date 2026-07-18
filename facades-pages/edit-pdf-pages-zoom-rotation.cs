using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade for page-level editing.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Example page modifications.
            editor.Zoom     = 0.75f; // 75 % zoom.
            editor.Rotation = 90;    // Rotate pages 90 degrees.

            // Apply the queued changes.
            editor.ApplyChanges();

            // Persist the edited document to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}