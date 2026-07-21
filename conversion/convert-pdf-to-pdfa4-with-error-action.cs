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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Configure conversion: target PDF/A‑4, attempt conversion of problematic elements
                // (ConvertErrorAction.None means do not delete objects that cannot be converted)
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_4, ConvertErrorAction.None);
                options.LogFileName = logPath; // optional log file for conversion messages

                // Perform the conversion
                bool conversionSucceeded = doc.Convert(options);
                Console.WriteLine($"Conversion succeeded: {conversionSucceeded}");

                // Save the converted PDF/A‑4 document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑4 saved to '{outputPath}'. Log written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}