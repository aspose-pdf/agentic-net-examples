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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the page editor and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Set the zoom factor to 1.2 (120%)
        editor.Zoom = 1.2f;

        // Determine all even‑numbered pages
        int totalPages = editor.GetPages();               // total page count
        List<int> evenPages = new List<int>();
        for (int i = 2; i <= totalPages; i += 2)
            evenPages.Add(i);

        // Apply the zoom only to the even pages
        editor.ProcessPages = evenPages.ToArray();        // pages to be edited

        // Apply the changes and save the result
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Zoom applied to even pages. Saved to '{outputPath}'.");
    }
}