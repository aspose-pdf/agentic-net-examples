using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
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
                // Configure conversion: PDF/A‑1b format, skip non‑convertible elements
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B, ConvertErrorAction.None)
                {
                    LogFileName = logPath // optional log file for conversion messages
                };

                // Perform the conversion
                doc.Convert(options);

                // Save the converted PDF/A document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑1b and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}