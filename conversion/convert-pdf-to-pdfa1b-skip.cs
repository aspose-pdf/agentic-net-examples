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
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑1b, skipping (deleting) non‑convertible elements
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                doc.Save(outputPath);
            }

            Console.WriteLine($"Converted to PDF/A‑1b saved as '{outputPath}'. Log: '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}