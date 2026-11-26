using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
        const string validationLogPath = "validation_log.txt";
        const string conversionLogPath = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Validate the PDF against PDF/A‑4 and write a validation report
                bool conforms = doc.Validate(validationLogPath, PdfFormat.PDF_A_4);
                Console.WriteLine($"Validation completed. Conforms to PDF/A‑4: {conforms}");

                // Convert the document to PDF/A‑4, writing a conversion log
                doc.Convert(conversionLogPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);

                // Save the PDF/A‑4 compliant file
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑4 file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
