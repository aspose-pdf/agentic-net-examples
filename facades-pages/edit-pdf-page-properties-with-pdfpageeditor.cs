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

        // PdfPageEditor edits page properties such as zoom, rotation, size, etc.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF file.
            editor.BindPdf(inputPath);

            // Example page modifications.
            editor.Zoom     = 0.8f; // 80% zoom.
            editor.Rotation = 90;   // Rotate all pages 90 degrees.

            // Apply the configured changes.
            editor.ApplyChanges();

            // Save the modified document to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}