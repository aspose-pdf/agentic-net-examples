using System;
using System.IO;
using Aspose.Pdf;

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

        // Enable automatic tagging for all documents processed in this application
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/A‑4, logging any conversion issues
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);

                // Save the PDF/A‑4 compliant document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑4 file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}