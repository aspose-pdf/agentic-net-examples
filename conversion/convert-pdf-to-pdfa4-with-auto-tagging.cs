using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Enable auto‑tagging globally before conversion
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        // Optional: configure heading detection strategy if needed
        // AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑4; auto‑tagging will be applied during conversion
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);

                // Save the resulting PDF/A‑4 document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑4 and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}