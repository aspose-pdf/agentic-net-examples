using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to edit page rotations
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Prepare a dictionary mapping page numbers to the desired rotation (270°)
            // Aspose.Pdf uses 1‑based page indexing
            int totalPages = editor.GetPages();
            var rotations = new Dictionary<int, int>();

            // Rotate every third page (3, 6, 9, ...) by 270 degrees
            for (int page = 3; page <= totalPages; page += 3)
            {
                rotations[page] = 270; // valid values: 0, 90, 180, 270
            }

            // Assign the rotation map to the editor
            editor.PageRotations = rotations;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
