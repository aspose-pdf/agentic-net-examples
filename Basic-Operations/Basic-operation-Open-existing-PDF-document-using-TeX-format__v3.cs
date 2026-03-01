using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source TeX file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input TeX file and output PDF file paths.
        string texPath = Path.Combine(dataDir, "input.tex");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the TeX source exists.
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // Initialize default TeX load options.
        TeXLoadOptions loadOptions = new TeXLoadOptions();

        // Load the TeX file, which automatically converts it to a PDF document.
        using (Document pdfDoc = new Document(texPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Converted TeX to PDF: {pdfPath}");
    }
}