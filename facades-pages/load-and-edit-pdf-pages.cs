using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable; wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF file for editing.
            editor.BindPdf(inputPath);

            // Example modifications (optional):
            // Rotate all pages 90 degrees (int value).
            editor.Rotation = 90; // integer degrees

            // Zoom pages to 120% (integer percentage).
            editor.Zoom = 120; // integer percentage (e.g., 120 = 120%)

            // Apply the pending changes.
            editor.ApplyChanges();

            // Save the edited document.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
