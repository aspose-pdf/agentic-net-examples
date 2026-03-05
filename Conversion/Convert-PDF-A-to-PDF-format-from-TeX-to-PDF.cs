using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the input TeX file and where the PDF will be saved.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input TeX file path.
        string texPath = Path.Combine(dataDir, "input.tex");

        // Output PDF file path.
        string pdfOutput = Path.Combine(dataDir, "output.pdf");

        // Verify that the TeX source exists.
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // Load the TeX file using TeXLoadOptions.
        TeXLoadOptions loadOptions = new TeXLoadOptions();

        // Use a using block to ensure the Document is disposed properly.
        using (Document doc = new Document(texPath, loadOptions))
        {
            // If the generated PDF is PDF/A, remove PDF/A compliance to obtain a regular PDF.
            doc.RemovePdfaCompliance();

            // Save the document as a standard PDF.
            doc.Save(pdfOutput);
        }

        Console.WriteLine($"TeX successfully converted to PDF: {pdfOutput}");
    }
}