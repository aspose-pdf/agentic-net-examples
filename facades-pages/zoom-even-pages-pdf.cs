using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable, so wrap it in a using block
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Get total number of pages (1‑based indexing)
            int totalPages = editor.GetPages();

            // Build a list of even‑numbered page indexes
            List<int> evenPages = new List<int>();
            for (int i = 2; i <= totalPages; i += 2)
                evenPages.Add(i);

            // Specify which pages the editor should process
            editor.ProcessPages = evenPages.ToArray();

            // Apply a zoom factor of 1.2 (120%)
            editor.Zoom = 1.2f;

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Even pages have been zoomed to 1.2x and saved as '{outputPath}'.");
    }
}