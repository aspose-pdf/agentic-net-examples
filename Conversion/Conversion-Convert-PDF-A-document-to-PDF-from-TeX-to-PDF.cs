using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per requirement to reference Facades namespace

class Program
{
    static void Main()
    {
        // Path to the directory containing input and output files.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input TeX file (PDF/A source is not needed for TeX conversion).
        string texPath = Path.Combine(dataDir, "source.tex");

        // Desired PDF output path.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the TeX source file exists.
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"Error: TeX source file not found at '{texPath}'.");
            return;
        }

        // Initialize load options for TeX files.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the TeX document and convert it to PDF.
        // Document is wrapped in a using block for deterministic disposal (lifecycle rule).
        using (Document pdfDoc = new Document(texPath, texLoadOptions))
        {
            // Save the document as a standard PDF.
            // No SaveOptions needed because we are saving to PDF format.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{pdfPath}'.");
    }
}