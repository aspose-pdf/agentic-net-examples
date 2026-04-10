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

        // Create PdfPageEditor, bind the source PDF, set rotation, apply changes, and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);          // Load the PDF
            editor.Rotation = 90;               // Allowed values: 0, 90, 180, 270
            editor.ApplyChanges();              // Apply the rotation to all pages
            editor.Save(outputPath);            // Save the modified PDF
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}