using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the LaTeX source file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input LaTeX file.
        string texFile = Path.Combine(dataDir, "sample.tex");

        // Desired output PDF file.
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        // Verify that the LaTeX source exists.
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"LaTeX file not found: {texFile}");
            return;
        }

        // Initialize TeX load options.
        // RasterizeFormulas = false keeps equations as vector graphics,
        // preserving quality and editability.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions
        {
            RasterizeFormulas = false,
            ShowTerminalOutput = true // optional: display LaTeX compiler output.
        };

        // Load the .tex file and convert it to a PDF document.
        // The Document constructor takes the file path and the load options.
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"LaTeX successfully converted to PDF: {pdfFile}");
    }
}