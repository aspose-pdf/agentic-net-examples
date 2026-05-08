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

        // Initialize PdfPageEditor, bind the source PDF, apply changes, and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF to be edited.
            editor.BindPdf(inputPath);

            // Example page modifications:
            // Set zoom factor (1.0 = 100%).
            editor.Zoom = 0.8f;

            // Rotate all pages by 90 degrees (allowed values: 0, 90, 180, 270).
            editor.Rotation = 90;

            // Apply the configured changes to the document.
            editor.ApplyChanges();

            // Save the edited PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}