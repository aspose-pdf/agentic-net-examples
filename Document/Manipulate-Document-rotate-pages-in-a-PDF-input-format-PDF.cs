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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to rotate pages. Wrap in using for deterministic disposal.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Specify which pages to rotate (1‑based indexing). Here we rotate all pages.
            // To rotate specific pages, e.g., pages 2 and 4: editor.ProcessPages = new int[] { 2, 4 };
            editor.ProcessPages = null; // null means all pages will be processed.

            // Set the rotation angle. Allowed values: 0, 90, 180, 270.
            editor.Rotation = 90; // rotate 90 degrees clockwise.

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}