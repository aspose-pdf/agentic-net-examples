using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_2x2_A5.pdf";

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
        var editor = new PdfFileEditor();

        // Create a 2×2 N‑up layout (4 original pages per new page).
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // portrait orientation for a 2×2 grid
        editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        // After the N‑up operation, set each page size to A5 as required.
        using (var doc = new Document(outputPath))
        {
            foreach (Page page in doc.Pages)
            {
                // Page.SetPageSize expects width and height as doubles, not a PageSize enum.
                page.SetPageSize(PageSize.A5.Width, PageSize.A5.Height);
            }
            doc.Save(outputPath);
        }

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
