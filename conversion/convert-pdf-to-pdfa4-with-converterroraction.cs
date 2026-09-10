using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
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
                // Convert to PDF/A‑4, keep problematic objects (ConvertErrorAction.None)
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.None);

                // Save the converted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'. Log written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}