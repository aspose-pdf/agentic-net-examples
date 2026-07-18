using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_page2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to rotate a specific page.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Set rotation for page 2 (pages are 1‑based). Value must be 0, 90, 180 or 270.
            editor.PageRotations = new Dictionary<int, int>
            {
                { 2, 90 }   // Rotate page 2 by 90 degrees clockwise.
            };

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 2 rotated and saved to '{outputPath}'.");
    }
}