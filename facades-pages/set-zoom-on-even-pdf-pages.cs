using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine the total number of pages using the Document API (ensures proper disposal)
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build an array with the even‑numbered page indexes (1‑based as required by PdfPageEditor)
        int[] evenPages = Enumerable.Range(1, pageCount)
                                    .Where(p => p % 2 == 0)
                                    .ToArray();

        // Apply the zoom to the selected pages via PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = evenPages;   // pages to which the settings will be applied
            editor.Zoom = 0.8f;                // 80 % zoom (uniform for all selected pages)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom set to 0.8 on even pages and saved to '{outputPath}'.");
    }
}
