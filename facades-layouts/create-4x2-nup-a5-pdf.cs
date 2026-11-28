using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_4x2_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare the source files array required by MakeNUp.
        string[] sourceFiles = new[] { inputPath };

        // MakeNUp creates an N‑up layout. The third argument specifies landscape orientation.
        // For a 4×2 portrait layout we pass false.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeNUp(sourceFiles, outputPath, false);
        if (!success)
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
            return;
        }

        // After the N‑up PDF is generated, set each page size to A5.
        Document nupDoc = new Document(outputPath);
        foreach (Page page in nupDoc.Pages)
        {
            // Page.SetPageSize expects width and height as doubles, not a PageSize enum.
            page.SetPageSize(PageSize.A5.Width, PageSize.A5.Height);
        }
        nupDoc.Save(outputPath);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
