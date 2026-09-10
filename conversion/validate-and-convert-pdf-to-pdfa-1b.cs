using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string validationLog  = "validation_log.xml";
        const string conversionLog  = "conversion_log.xml";
        const string outputPath     = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Validate against PDF/A‑1b and write XML log
                bool isValid = doc.Validate(validationLog, PdfFormat.PDF_A_1B);
                Console.WriteLine($"Validation result: {isValid}");

                // Convert to PDF/A‑1b, logging conversion details
                doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the PDF/A‑1b compliant document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}