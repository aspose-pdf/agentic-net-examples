using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑1b, removing any conversion errors
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                // Save the PDF/A‑1b compliant document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑1b saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}