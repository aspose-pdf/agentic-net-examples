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
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create conversion options for PDF/A‑3b.
                // ConvertErrorAction.None forces the conversion process to throw a ConvertException
                // when an object cannot be converted, instead of silently deleting it.
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(
                    logPath,
                    PdfFormat.PDF_A_3B,
                    ConvertErrorAction.None);

                // Perform the conversion. If any conversion error occurs, a ConvertException will be thrown.
                bool result = doc.Convert(options);

                // If conversion succeeded (result == true) we can save the PDF/A‑3b document.
                if (result)
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF/A‑3b saved to '{outputPath}'.");
                }
                else
                {
                    Console.WriteLine("Conversion completed with warnings (check the log file).");
                }
            }
        }
        catch (ConvertException ex)
        {
            // Handle conversion errors that were forced to be thrown.
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}