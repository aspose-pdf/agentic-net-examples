using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output-pdfa.pdf";
        const string originalValidationLog = "original_validation.xml";
        const string conversionLog = "conversion_log.xml";
        const string pdfaValidationLog = "pdfa_validation.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate the original PDF against PDF/A‑1b rules
            bool originalValid = doc.Validate(originalValidationLog, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Original PDF validation result: {originalValid}");

            // Convert the document to PDF/A‑1b; conversion errors are written to conversionLog
            doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Validate the converted PDF/A‑1b document
            bool pdfaValid = doc.Validate(pdfaValidationLog, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Converted PDF/A‑1b validation result: {pdfaValid}");

            // Save the PDF/A‑1b compliant file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
    }
}