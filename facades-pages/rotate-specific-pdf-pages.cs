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

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (facade) to edit page rotations
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Define rotations: pages 3, 5 and 7 -> 180 degrees
            var rotations = new Dictionary<int, int>
            {
                { 3, 180 },
                { 5, 180 },
                { 7, 180 }
            };
            editor.PageRotations = rotations;

            // Apply the rotation changes
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}