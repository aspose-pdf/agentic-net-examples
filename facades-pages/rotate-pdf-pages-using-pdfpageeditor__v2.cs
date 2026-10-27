using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF into the editor
            editor.BindPdf(inputPath);

            // Specify which page(s) to rotate (1‑based indexing)
            editor.ProcessPages = new int[] { 1 };

            // Set rotation angle in degrees. Allowed values are 0, 90, 180, or 270.
            editor.Rotation = 90;

            // Save the rotated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}