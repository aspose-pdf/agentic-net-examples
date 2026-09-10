using System;
using System.Collections.Generic;
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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to edit page properties
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF file
            editor.BindPdf(inputPath);

            // Determine total pages (1‑based indexing)
            int totalPages = editor.GetPages();

            // Build an int[] containing only even‑numbered pages
            List<int> evenPages = new List<int>();
            for (int i = 2; i <= totalPages; i += 2)
            {
                evenPages.Add(i);
            }
            editor.ProcessPages = evenPages.ToArray(); // PdfPageEditor expects int[]

            // Set zoom factor to 0.8 (80%)
            editor.Zoom = 0.8f;

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied to even pages. Saved as '{outputPath}'.");
    }
}
