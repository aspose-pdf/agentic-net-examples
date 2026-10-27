using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;               // Document, page counting
using Aspose.Pdf.Facades;      // PdfPageEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine total page count (Aspose.Pdf uses 1‑based indexing)
        int pageCount;
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Collect all odd‑numbered page indices
        List<int> oddPages = new List<int>();
        for (int i = 1; i <= pageCount; i += 2)   // step by 2 → 1,3,5,...
        {
            oddPages.Add(i);
        }

        // Apply a 1.2 (120 %) zoom to the selected odd pages
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);                 // load source PDF
            editor.ProcessPages = oddPages.ToArray();  // target pages
            editor.Zoom = 1.2f;                        // set zoom factor
            editor.Save(outputPath);                   // write result
        }

        Console.WriteLine($"Zoom of 1.2 applied to odd pages. Output saved to '{outputPath}'.");
    }
}