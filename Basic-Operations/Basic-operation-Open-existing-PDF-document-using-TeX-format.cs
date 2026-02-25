using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, TeXLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string dataDir   = @"YOUR_DATA_DIRECTORY";
        string texFile          = Path.Combine(dataDir, "sample.tex");   // Input TeX file
        string pdfOutputPath    = Path.Combine(dataDir, "sample_converted.pdf"); // Output PDF

        // Verify input file exists
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        // Initialize load options for TeX conversion
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Open the TeX file and convert it to a PDF document
        // NOTE: Document implements IDisposable – wrap in using for deterministic disposal
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfOutputPath);
        }

        Console.WriteLine($"TeX file successfully converted to PDF: {pdfOutputPath}");
    }
}