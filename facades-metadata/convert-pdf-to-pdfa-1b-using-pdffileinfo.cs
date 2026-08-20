using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF via PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Access the underlying Document object
            using (Document doc = pdfInfo.Document)
            {
                // Convert the document to PDF/A‑1B (archival compliant)
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            }

            // Save the updated (PDF/A compliant) document using the facade
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"PDF/A compliant file saved to '{outputPath}'.");
    }
}