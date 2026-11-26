using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input3.pdf";
        const string outputPath = "output_pdfx1a.pdf";
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
                // Convert to PDF/X-1a:2001 using the default output‑intent profile.
                // The log file records any conversion issues.
                doc.Convert(logPath, PdfFormat.PDF_X_1A, ConvertErrorAction.Delete);

                // Save the compliant PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/X-1a:2001 compliant file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}