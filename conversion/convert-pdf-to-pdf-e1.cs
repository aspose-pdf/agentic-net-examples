using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "brochureG.pdf";
        const string outputPath = "brochureG_PDFE1.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF and convert it to PDF/E‑1 format.
        using (Document doc = new Document(inputPath))
        {
            // Convert to PDF/E‑1; no additional conversion options are required.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Conversion completed. PDF/E‑1 saved to '{outputPath}'.");
    }
}