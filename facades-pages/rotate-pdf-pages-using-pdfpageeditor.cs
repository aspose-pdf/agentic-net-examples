using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade for page-level operations (rotate, zoom, etc.)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF into the editor
            editor.BindPdf(inputPath);

            // Set rotation for all pages.
            // Allowed values are 0, 90, 180, or 270 degrees.
            editor.Rotation = 90;

            // Apply the pending changes to the underlying document
            editor.ApplyChanges();

            // Save the modified PDF. No SaveOptions needed for PDF output.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}