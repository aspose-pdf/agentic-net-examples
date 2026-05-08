using System;
using System.Collections.Generic;
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

        // Use PdfPageEditor facade to edit page rotations.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Define rotations for specific pages (1‑based indexing).
            var rotations = new Dictionary<int, int>
            {
                { 3, 180 },
                { 5, 180 },
                { 7, 180 }
            };

            // Assign the rotations to the editor.
            editor.PageRotations = rotations;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Pages 3, 5, and 7 rotated 180° and saved to '{outputPath}'.");
    }
}