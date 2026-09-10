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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑3b.
                // ConvertErrorAction.None causes conversion to fail with an exception
                // when an object cannot be converted (i.e., "throw" behavior).
                bool success = doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.None);

                // If conversion succeeded, save the resulting PDF/A‑3b file.
                if (success)
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF/A‑3b saved to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Conversion failed; see log for details.");
                }
            }
        }
        catch (ConvertException ex)
        {
            // Raised when conversion encounters an unrecoverable error.
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}