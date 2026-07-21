using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
        const string logPath    = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Create conversion options for PDF/A‑3B.
                // ConvertErrorAction.None will cause the conversion to throw on errors.
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(
                    logPath,               // log file for conversion messages
                    PdfFormat.PDF_A_3B,    // target PDF/A‑3B format
                    ConvertErrorAction.None // raise exceptions for conversion errors
                );

                // Perform the conversion. Returns true if successful; otherwise a ConvertException is thrown.
                bool converted = doc.Convert(options);

                if (converted)
                {
                    // Save the converted PDF/A‑3B document.
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF/A‑3B saved to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Conversion reported failure without exception.");
                }
            }
        }
        catch (ConvertException ex)
        {
            // Handle conversion‑specific errors.
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}