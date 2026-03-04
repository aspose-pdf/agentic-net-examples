using System;
using System.IO;
using Aspose.Pdf;   // Aspose.Pdf namespace contains Document, TeXLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Directory that contains the source TeX file and where the PDF will be written.
        // Adjust this path to point to a real folder on your machine.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Full path to the TeX source file.
        string texFile = Path.Combine(dataDir, "TeX-to-PDF.tex");

        // Full path for the resulting PDF file.
        string pdfFile = Path.Combine(dataDir, "Tex-to-PDF.pdf");

        // Verify that the TeX source file exists before attempting to load it.
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"Error: TeX source file not found: {texFile}");
            return;
        }

        // Initialize TeX load options (default settings are sufficient for most cases).
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the TeX file and convert it to a PDF document.
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the generated PDF to the specified location.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"TeX file successfully converted to PDF: {pdfFile}");
    }
}