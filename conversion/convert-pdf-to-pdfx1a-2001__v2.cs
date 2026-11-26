using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "manualM.pdf";
        const string outputPath     = "manualM_PDFX1a.pdf";
        const string conversionLog  = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/X-1a (2001) format.
                // The Convert method writes a log file and applies the specified PDF format.
                doc.Convert(conversionLog, PdfFormat.PDF_X_1A, ConvertErrorAction.Delete);

                // Save the converted PDF. No SaveOptions needed because the output is PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}