using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the LaTeX source file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input LaTeX file (.tex).
        string texFile = Path.Combine(dataDir, "sample.tex");

        // Desired output PDF file.
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        // Verify that the source file exists.
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"LaTeX file not found: {texFile}");
            return;
        }

        // Initialize TeXLoadOptions.
        // Leaving RasterizeFormulas = false (default) preserves equations as vector graphics.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the LaTeX file and convert it to a PDF document.
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"LaTeX file successfully converted to PDF: {pdfFile}");
    }
}