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

        // Initialize PdfPageEditor, bind the source PDF, apply desired changes, and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF file into the editor.
            editor.BindPdf(inputPath);

            // Example page modifications:
            editor.Zoom = 0.8f;          // Set zoom to 80%
            editor.Rotation = 90;       // Rotate all pages by 90 degrees

            // Apply the modifications to the document.
            editor.ApplyChanges();

            // Save the edited PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}