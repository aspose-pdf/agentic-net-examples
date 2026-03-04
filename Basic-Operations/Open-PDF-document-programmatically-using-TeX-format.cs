using System;
using System.IO;
using Aspose.Pdf; // TeXLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Directory containing the source TeX file and where the PDF will be saved
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input TeX file path
        string texFile = Path.Combine(dataDir, "sample.tex");
        // Output PDF file path
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        // Verify that the TeX source exists
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        // Initialize load options for TeX conversion (default settings)
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the TeX file and convert it to a PDF document
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"Conversion completed: {pdfFile}");
    }
}