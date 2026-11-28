using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_7x1_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a 7×1 N‑up layout (portrait) using PdfFileEditor.
        PdfFileEditor editor = new PdfFileEditor();
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // false => portrait (rows > columns)
        editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        // Adjust each page size to A5. SetPageSize expects width and height doubles.
        Document result = new Document(outputPath);
        foreach (Page page in result.Pages)
        {
            // Use the Width and Height properties of the PageSize enum.
            page.SetPageSize((double)PageSize.A5.Width, (double)PageSize.A5.Height);
        }
        result.Save(outputPath);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
