using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfPageEditor facade, bind the source PDF,
        // set zoom on even‑numbered pages, apply changes and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF file.
            editor.BindPdf(inputPath);

            // Get total page count (1‑based indexing).
            int totalPages = editor.GetPages();

            // Build an array of even page numbers.
            List<int> evenPages = new List<int>();
            for (int i = 2; i <= totalPages; i += 2)
                evenPages.Add(i);
            editor.ProcessPages = evenPages.ToArray();

            // Apply a zoom factor of 1.2 (120%).
            editor.Zoom = 1.2f;

            // Commit the modifications.
            editor.ApplyChanges();

            // Save the edited PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Even pages zoomed to 1.2x and saved as '{outputPath}'.");
    }
}