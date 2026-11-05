using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string validationXmlPath = "validation_report.xml";
        const string pdfAOutputPath = "output_pdfa1b.pdf";
        const string conversionLogPath = "conversion_log.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdfPath))
            {
                // -----------------------------------------------------------------
                // 1. Validate the PDF/A‑1b compliance and write the validation report to XML.
                // -----------------------------------------------------------------
                bool isValid = doc.Validate(validationXmlPath, PdfFormat.PDF_A_1B);
                Console.WriteLine($"Validation report saved to '{validationXmlPath}'. Valid: {isValid}");

                // -----------------------------------------------------------------
                // 2. Convert the document to PDF/A‑1b (if it is not already compliant).
                // -----------------------------------------------------------------
                doc.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                Console.WriteLine($"Conversion to PDF/A‑1b completed. Log saved to '{conversionLogPath}'.");

                // -----------------------------------------------------------------
                // 3. Save the PDF/A‑1b document.
                // -----------------------------------------------------------------
                doc.Save(pdfAOutputPath);
                Console.WriteLine($"PDF/A‑1b file saved to '{pdfAOutputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
