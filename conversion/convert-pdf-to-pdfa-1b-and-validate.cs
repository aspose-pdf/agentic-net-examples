using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath        = "input.pdf";          // source PDF
        const string outputPdfAPath   = "output_pdfa1b.pdf";  // PDF/A‑1b result
        const string conversionLog    = "conversion_log.txt"; // conversion log file
        const string validationLog    = "validation_log.txt"; // validation log file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/A‑1b.
                // The method returns true if conversion succeeded.
                bool conversionResult = doc.Convert(
                    conversionLog,          // path to conversion log
                    PdfFormat.PDF_A_1B,     // target format
                    ConvertErrorAction.Delete); // action for objects that cannot be converted

                Console.WriteLine($"Conversion succeeded: {conversionResult}");

                // Validate the converted document against PDF/A‑1b.
                // Validation messages are written to the specified log file.
                bool isValid = doc.Validate(validationLog, PdfFormat.PDF_A_1B);
                Console.WriteLine($"Validation succeeded: {isValid}");

                // Save the PDF/A‑1b compliant document.
                doc.Save(outputPdfAPath);
            }

            Console.WriteLine($"PDF/A‑1b file saved to '{outputPdfAPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}