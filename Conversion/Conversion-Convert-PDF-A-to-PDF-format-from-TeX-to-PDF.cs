using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the input TeX file (assumed to be PDF/A compliant source)
        string texFilePath = Path.Combine("Data", "sample.tex");
        // Path where the resulting PDF will be saved
        string outputPdfPath = Path.Combine("Data", "result.pdf");

        // Verify that the TeX source file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"Input TeX file not found: {texFilePath}");
            return;
        }

        try
        {
            // Initialize TeX loading options (default settings)
            TeXLoadOptions loadOptions = new TeXLoadOptions();

            // Load the TeX file into a PDF document
            using (Document pdfDocument = new Document(texFilePath, loadOptions))
            {
                // Save the document as a regular PDF (non‑PDF/A)
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"Conversion completed successfully. PDF saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
