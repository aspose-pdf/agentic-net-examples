using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and conversion log
        const string outputPath = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.xml";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add three blank pages
            doc.Pages.Add(); // Page 1
            doc.Pages.Add(); // Page 2
            doc.Pages.Add(); // Page 3

            // Convert the document to PDF/X‑3 format
            // The Convert method writes a log file; the log path can be any writable location
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted document as a regular PDF file (it now conforms to PDF/X‑3)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 document saved to '{outputPath}'.");
    }
}