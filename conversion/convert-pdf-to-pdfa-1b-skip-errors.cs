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
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑1b, skipping (ignoring) elements that cannot be converted
                // ConvertErrorAction.None corresponds to the "skip" behavior
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.None);

                // Save the converted PDF/A‑1b document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Converted PDF/A‑1b saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}