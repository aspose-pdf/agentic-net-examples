using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "manualF.pdf";
        const string outputPath = "manualF_PDFX1a.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF.
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/X-1a:2003. No external ICC profile is provided.
                doc.Convert(logPath, PdfFormat.PDF_X_1A, ConvertErrorAction.Delete);

                // Save the converted PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}