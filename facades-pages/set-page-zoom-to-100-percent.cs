using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create PdfPageEditor facade, bind the PDF, set zoom, apply changes, and save
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Set zoom coefficient to 1.0 (100%) for all pages
            editor.Zoom = 1.0f;

            // Apply the modifications to the document
            editor.ApplyChanges();

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom set to 1.0 for all pages. Saved to '{outputPath}'.");
    }
}