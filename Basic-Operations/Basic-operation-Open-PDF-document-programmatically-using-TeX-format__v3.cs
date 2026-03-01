using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the TeX file.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input TeX file and output PDF file.
        string texFile = Path.Combine(dataDir, "sample.tex");
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        // Verify that the TeX source exists.
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        // Initialize TeX load options (default settings).
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the TeX file into a PDF document using the constructor that accepts LoadOptions.
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the generated PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"Successfully converted '{texFile}' to PDF '{pdfFile}'.");
    }
}