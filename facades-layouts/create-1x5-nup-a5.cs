using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_1x5_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a 1×5 N‑up layout.
        // In older Aspose.Pdf versions NUpOptions and PdfFileEditor.NUp are unavailable.
        // Use PdfFileEditor.MakeNUp (source files, output path, isLandscape) and then set the page size.
        PdfFileEditor editor = new PdfFileEditor();
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // portrait orientation for a 1×5 layout
        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        if (!success)
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
            return;
        }

        // Adjust each resulting page to A5 size.
        Document nupDoc = new Document(outputPath);
        foreach (Page page in nupDoc.Pages)
        {
            // PageSize.A5 provides Width and Height in points.
            page.PageInfo.Width = PageSize.A5.Width;
            page.PageInfo.Height = PageSize.A5.Height;
        }
        nupDoc.Save(outputPath);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
