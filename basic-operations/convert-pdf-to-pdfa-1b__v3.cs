using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath    = "conversion_log.xml";

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
                // Convert to PDF/A‑1B compliance.
                // ConvertErrorAction.Delete removes objects that cannot be converted.
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the converted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑1B compliant file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}