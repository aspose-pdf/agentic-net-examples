using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ConvertErrorAction enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Enable global auto‑tagging for all documents loaded thereafter
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/A‑3b. A log file path and error handling mode are required.
            doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the converted PDF/A‑3b document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3b file saved to '{outputPath}'.");
    }
}