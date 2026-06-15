using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Select non‑consecutive pages (1‑based indexing)
            editor.ProcessPages = new int[] { 1, 3, 5, 7 };

            // Apply a common zoom factor (e.g., 150 %)
            editor.Zoom = 1.5f; // float literal required by the API

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied to selected pages. Saved as '{outputPath}'.");
    }
}
