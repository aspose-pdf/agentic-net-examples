using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath          = "input.pdf";
        const string outputPdfAPath      = "output_pdfa1b.pdf";
        const string conversionLogPath   = "conversion_log.txt";
        const string validationLogPath   = "validation_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/A‑1b, logging conversion errors
            bool conversionResult = doc.Convert(
                conversionLogPath,          // path for conversion log
                PdfFormat.PDF_A_1B,         // target format
                ConvertErrorAction.Delete   // action for objects that cannot be converted
            );

            Console.WriteLine($"Conversion succeeded: {conversionResult}");

            // Validate the resulting PDF/A‑1b document, logging validation results
            bool validationResult = doc.Validate(
                validationLogPath,          // path for validation log (XML)
                PdfFormat.PDF_A_1B          // format to validate against
            );

            Console.WriteLine($"Validation succeeded: {validationResult}");

            // Save the PDF/A‑1b compliant document
            doc.Save(outputPdfAPath);
            Console.WriteLine($"PDF/A‑1b file saved to '{outputPdfAPath}'.");
        }
    }
}