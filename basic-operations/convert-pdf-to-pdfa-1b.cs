using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";

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
                // Set up conversion options for PDF/A‑1b
                // - Format: PDF_A_1B (PDF/A‑1b)
                // - ErrorAction: Delete objects that cannot be converted
                // - Keep default settings to preserve structure and embedded fonts
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(
                    PdfFormat.PDF_A_1B,               // target PDF/A format
                    ConvertErrorAction.Delete);       // remove unconvertible objects

                // Perform the conversion
                bool success = doc.Convert(options);
                if (!success)
                {
                    Console.Error.WriteLine("Conversion reported failures. Check the log if needed.");
                }

                // Save the converted document
                doc.Save(outputPath);
                Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}