using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF file to the page editor facade
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Set the zoom factor (1.0 = 100%). 1.2 = 120%
        editor.Zoom = 1.2f;

        // Determine the total number of pages
        int totalPages = editor.GetPages();

        // Build an array of odd‑numbered page indexes (1‑based)
        int[] oddPages = GetOddPageNumbers(totalPages);

        // Apply the zoom only to the odd pages
        editor.ProcessPages = oddPages;

        // Apply the changes and save the result
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Zoomed PDF saved to '{outputPath}'.");
    }

    // Helper method to generate an int[] containing odd page numbers
    static int[] GetOddPageNumbers(int totalPages)
    {
        var list = new System.Collections.Generic.List<int>();
        for (int i = 1; i <= totalPages; i += 2)
        {
            list.Add(i);
        }
        return list.ToArray();
    }
}