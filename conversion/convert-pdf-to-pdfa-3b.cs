using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
        const string logPath    = "conversion.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block (ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑3b.
                // ConvertErrorAction.None is the only available option;
                // it does not suppress errors, so conversion errors will raise exceptions.
                bool conversionResult = doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.None);

                // If the method returns false, conversion failed without throwing.
                // You may handle this case as needed.
                if (!conversionResult)
                {
                    Console.Error.WriteLine("Conversion completed with non‑fatal issues (see log).");
                }

                // Save the converted document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑3b and saved as '{outputPath}'.");
        }
        catch (ConvertException ex)
        {
            // Conversion errors are reported via ConvertException.
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}