using System;
using System.IO;
using System.Collections.Generic;
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

        // Use PdfPageEditor (Facade) to rotate specific pages.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Determine total number of pages (1‑based indexing).
            int totalPages = editor.GetPages();

            // Collect every third page number (3, 6, 9, ...).
            List<int> pagesToRotate = new List<int>();
            for (int i = 3; i <= totalPages; i += 3)
                pagesToRotate.Add(i);

            // Specify which pages the editor should process.
            editor.ProcessPages = pagesToRotate.ToArray();

            // Set the desired rotation (must be 0, 90, 180, or 270).
            editor.Rotation = 270;

            // Apply the rotation changes.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}