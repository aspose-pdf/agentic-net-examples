using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "manualE.pdf";
        const string outputPath = "manualE_PDFE1.pdf";
        const string logPath    = "conversion_log.xml";

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
                // Convert to PDF/E‑1 format; log any conversion issues
                doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the PDF/E‑1 compliant document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully converted to PDF/E‑1: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}