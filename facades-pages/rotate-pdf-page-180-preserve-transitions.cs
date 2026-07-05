using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";
        const int    pageNumber = 1; // page to rotate (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Preserve any existing transition settings by not modifying them.
            // Apply a 180° rotation only to the specified page.
            editor.PageRotations = new Dictionary<int, int>
            {
                { pageNumber, 180 }
            };

            // Commit the changes to the document.
            editor.ApplyChanges();

            // Save the edited PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page {pageNumber} rotated 180° and saved to '{outputPath}'.");
    }
}