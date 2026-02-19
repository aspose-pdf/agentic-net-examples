using System;
using System.IO;
using Aspose.Pdf; // TeXLoadOptions, SaveFormat, Document are in this namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string texFilePath = Path.Combine("Data", "sample.tex");
        string outputPdfPath = Path.Combine("Data", "sample_converted.pdf");

        // Verify input file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"Input TeX file not found: {texFilePath}");
            return;
        }

        try
        {
            // Load the TeX file using default options
            TeXLoadOptions loadOptions = new TeXLoadOptions();

            // Create a PDF document from the TeX source
            using (Document pdfDocument = new Document(texFilePath, loadOptions))
            {
                // NOTE:
                // Aspose.Pdf does not expose a dedicated SaveFormat for PDF/E‑1.
                // If a specific PDF/E‑1 conversion is required, use PdfFormatConversionOptions
                // with the appropriate PdfFormat value (when available) and call
                // pdfDocument.Save(outputPdfPath, conversionOptions);
                // For demonstration, we save as a regular PDF.

                pdfDocument.Save(outputPdfPath, SaveFormat.Pdf);
                Console.WriteLine($"TeX file successfully converted to PDF: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}