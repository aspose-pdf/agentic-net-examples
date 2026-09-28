using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string validationLogPath = "validation_result.xml";
        const string conversionLogPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // -------------------------------------------------
                // 1. Validate the document against PDF/A‑1b standard
                // -------------------------------------------------
                // Document.Validate writes an XML log and returns a bool indicating compliance.
                bool isValid = doc.Validate(validationLogPath, PdfFormat.PDF_A_1B);
                Console.WriteLine($"Validation log saved to '{validationLogPath}'.");
                Console.WriteLine($"Is PDF/A‑1b compliant? {isValid}");

                // -------------------------------------------------
                // 2. Convert the document to PDF/A‑1b
                // -------------------------------------------------
                // The Convert method creates a conversion log (XML) that can be inspected if needed
                doc.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // -------------------------------------------------
                // 3. Save the converted PDF/A‑1b document
                // -------------------------------------------------
                doc.Save(outputPath);
                Console.WriteLine($"PDF/A‑1b document saved to '{outputPath}'.");
                Console.WriteLine($"Conversion log saved to '{conversionLogPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
