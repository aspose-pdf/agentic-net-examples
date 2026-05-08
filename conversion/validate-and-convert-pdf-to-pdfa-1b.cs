using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string validationLog = "validation_log.xml";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Validate against PDF/A‑1b and write the XML log
                bool isValid = doc.Validate(validationLog, PdfFormat.PDF_A_1B);
                Console.WriteLine($"Validation result: {(isValid ? "OK" : "Issues found")} (log: {validationLog})");

                // Convert the document to PDF/A‑1b, logging conversion details
                doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                Console.WriteLine($"Conversion completed (log: {conversionLog})");

                // Save the PDF/A‑1b compliant document
                doc.Save(outputPath);
                Console.WriteLine($"PDF/A‑1b saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}