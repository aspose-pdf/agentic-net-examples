using System;
using System.IO;
using Aspose.Pdf; // TeXLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        string texFile = Path.Combine(dataDir, "sample.tex");
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        // Verify input file exists
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"LaTeX source not found: {texFile}");
            return;
        }

        // Initialize TeXLoadOptions – you can tweak properties here if required
        TeXLoadOptions texLoadOptions = new TeXLoadOptions
        {
            // Example: keep formulas as vectors (set false to rasterize)
            RasterizeFormulas = false,
            // Show LaTeX compiler output in console (optional)
            ShowTerminalOutput = true
        };

        // Load the .tex file and convert to PDF
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"LaTeX file converted successfully: {pdfFile}");
    }
}