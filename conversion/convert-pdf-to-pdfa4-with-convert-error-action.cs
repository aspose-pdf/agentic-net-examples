using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
        const string logPath = "conversion_log.txt";

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
                // Create conversion options for PDF/A‑4.
                // ConvertErrorAction is set to None to attempt conversion of problematic elements.
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(
                    logPath,               // log file for conversion messages
                    PdfFormat.PDF_A_4,     // target PDF/A‑4 format
                    ConvertErrorAction.None); // attempt conversion (do not delete problematic objects)

                // Perform the conversion
                bool conversionResult = doc.Convert(options);
                Console.WriteLine($"Conversion succeeded: {conversionResult}");

                // Save the converted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑4 document saved to '{outputPath}'. Log written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}