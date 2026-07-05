using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_even_zoom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the page editor and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Determine total number of pages (1‑based indexing)
        int totalPages = editor.GetPages();

        // Collect all even‑numbered page indices
        List<int> evenPages = new List<int>();
        for (int i = 2; i <= totalPages; i += 2)
            evenPages.Add(i);

        // Specify which pages to edit
        editor.ProcessPages = evenPages.ToArray();

        // Apply a zoom factor of 1.2 (120%)
        editor.Zoom = 1.2f;

        // Commit the changes to the selected pages
        editor.ApplyChanges();

        // Save the modified document
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Even pages have been zoomed to 1.2× and saved as '{outputPath}'.");
    }
}