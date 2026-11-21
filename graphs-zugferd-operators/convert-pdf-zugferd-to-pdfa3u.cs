using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "invoice_with_zugferd.pdf";
        const string outputPdf = "invoice_pdfa3u.pdf";
        const string logPath   = "conversion_log.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF (contains ZUGFeRD XML attachment)
            using (Document doc = new Document(inputPdf))
            {
                // Convert to PDF/A‑3u. The Convert method preserves embedded files (e.g., the ZUGFeRD XML)
                doc.Convert(logPath, PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);

                // Save the converted document. After conversion the document is already PDF/A‑3u compliant.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion completed. PDF/A‑3u saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}