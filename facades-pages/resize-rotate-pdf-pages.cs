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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade for page-level editing.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Example modifications:
            // - Reduce page size to 80% of original.
            // - Rotate all pages by 90 degrees.
            editor.Zoom     = 0.8f;
            editor.Rotation = 90; // valid values: 0, 90, 180, 270

            // Apply the queued changes.
            editor.ApplyChanges();

            // Persist the edited document to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}