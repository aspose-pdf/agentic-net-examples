using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source TeX file and where the PDF will be saved.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Path to the TeX file to be opened.
        string texFile = Path.Combine(dataDir, "sample.tex");

        // Desired output PDF file path.
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the TeX source exists.
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        // Initialize load options for TeX conversion.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Open the TeX file and convert it to a PDF document.
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"TeX file successfully converted to PDF: {pdfFile}");
    }
}