using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the TeX source file
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input TeX file and output PDF file paths
        string texPath = Path.Combine(dataDir, "sample.tex");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the TeX file exists
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // Configure TeX loading options (customize as needed)
        TeXLoadOptions texOptions = new TeXLoadOptions
        {
            // Example: show LaTeX compiler output in the console
            ShowTerminalOutput = true
        };

        // Load the TeX file into a PDF document
        using (Document pdfDocument = new Document(texPath, texOptions))
        {
            // Save the generated PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF successfully created at: {pdfPath}");
    }
}