using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string dataDir   = @"YOUR_DATA_DIRECTORY";
        string texFile         = Path.Combine(dataDir, "input.tex");
        string pdfOutputFile   = Path.Combine(dataDir, "output.pdf");

        // Verify source file exists
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX source not found: {texFile}");
            return;
        }

        // Load the TeX file using TeXLoadOptions
        TeXLoadOptions loadOptions = new TeXLoadOptions();

        // Document constructor loads the TeX file and creates a PDF in memory
        using (Document pdfDocument = new Document(texFile, loadOptions))
        {
            // Save the resulting PDF (regular PDF, not PDF/A)
            pdfDocument.Save(pdfOutputFile);
        }

        Console.WriteLine($"TeX file converted to PDF: {pdfOutputFile}");
    }
}