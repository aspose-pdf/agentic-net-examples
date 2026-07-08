using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string validationLogPath = "validation.xml";
        const string conversionLogPath = "conversion.xml";
        const string outputPdfPath     = "output_pdfa1b.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Validate the document against PDF/A‑1b and write the XML log
            bool isValid = doc.Validate(validationLogPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Validation completed. Result: {isValid}");

            // Convert the document to PDF/A‑1b, logging any conversion issues
            bool isConverted = doc.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            Console.WriteLine($"Conversion completed. Result: {isConverted}");

            // Save the resulting PDF/A‑1b file
            doc.Save(outputPdfPath);
            Console.WriteLine($"PDF/A‑1b file saved to: {outputPdfPath}");
        }
    }
}