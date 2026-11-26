using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
        const string logPath    = "validation_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document once and use it for validation and conversion
        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Validate the source PDF (generic validation – using PDF/A‑1b as a baseline)
                // The Validate method writes a detailed XML report to logPath and returns a bool.
                bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);

                if (!isValid)
                {
                    Console.Error.WriteLine("PDF validation failed. See the log for details.");
                }
                else
                {
                    Console.WriteLine("PDF validation succeeded.");
                }

                // Convert to PDF/A‑3b and save the result
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
                doc.Save(outputPath);

                Console.WriteLine($"PDF/A‑3b file saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation or conversion: {ex.Message}");
        }
    }
}
