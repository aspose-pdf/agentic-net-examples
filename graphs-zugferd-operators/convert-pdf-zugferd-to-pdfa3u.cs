using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF with ZUGFeRD attachment
        const string outputPdf = "output_pdfa3u.pdf";   // Resulting PDF/A‑3u file
        const string logFile   = "conversion.log";     // Optional conversion log

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF (ZUGFeRD attachment is kept in the document object)
        using (Document doc = new Document(inputPdf))
        {
            // Convert to PDF/A‑3u. Attachments (including the ZUGFeRD XML) are preserved by default.
            // ConvertErrorAction.Delete removes objects that cannot be converted.
            doc.Convert(logFile, PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑3u file saved to '{outputPdf}'.");
    }
}