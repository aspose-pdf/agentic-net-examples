using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory that contains the LaTeX source file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input LaTeX file and output PDF file.
        string texFile = Path.Combine(dataDir, "sample.tex");
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"LaTeX file not found: {texFile}");
            return;
        }

        // Configure TeX loading options.
        // RasterizeFormulas = false keeps formulas as vector graphics.
        // ShowTerminalOutput = true prints the LaTeX compiler messages.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions
        {
            RasterizeFormulas = false,
            ShowTerminalOutput = true
        };

        // Load the .tex file and convert it to PDF.
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the generated PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"LaTeX successfully converted to PDF: {pdfFile}");
    }
}