using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor facade and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Use a generic Dictionary<int,int> for page‑rotation mapping (required by PageRotations)
        var rotations = new Dictionary<int, int>();

        // Get total number of pages (1‑based indexing)
        int totalPages = editor.GetPages();

        // Rotate every third page by 270 degrees
        for (int i = 1; i <= totalPages; i++)
        {
            if (i % 3 == 0)
            {
                rotations[i] = 270; // valid values: 0, 90, 180, 270
            }
        }

        // Assign the rotation map to the editor
        editor.PageRotations = rotations;

        // Apply the changes and save the result
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Rotated every third page saved to '{outputPath}'.");
    }
}
