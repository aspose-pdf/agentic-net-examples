using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_2x4_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a 2×4 N‑up layout. The third argument specifies portrait (false) or landscape (true).
        var editor = new PdfFileEditor();
        editor.MakeNUp(new[] { inputPath }, outputPath, false);

        // Resize each generated page to A5 size.
        using (var doc = new Document(outputPath))
        {
            foreach (Page page in doc.Pages)
            {
                // SetPageSize expects width and height as doubles, not a PageSize enum.
                page.SetPageSize(PageSize.A5.Width, PageSize.A5.Height);
            }
            doc.Save(outputPath);
        }

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
