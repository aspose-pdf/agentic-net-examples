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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PDF page editor facade
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);               // Load the PDF

        // Determine total number of pages
        int totalPages = editor.GetPages();      // 1‑based page count

        // Build a list of even‑numbered pages (2,4,6,…)
        List<int> evenPages = new List<int>();
        for (int i = 2; i <= totalPages; i += 2)
            evenPages.Add(i);

        // Apply the operation only to the even pages
        editor.ProcessPages = evenPages.ToArray();

        // Set zoom factor to 1.2 (120%)
        editor.Zoom = 1.2f;

        // Apply the changes to the selected pages
        editor.ApplyChanges();

        // Save the modified PDF
        editor.Save(outputPath);

        // Clean up resources
        editor.Close();
    }
}