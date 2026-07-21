using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.xml";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add three blank pages
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Convert the document to PDF/X‑3 compliance
            // This uses the overload that writes conversion errors to a log file
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X‑3 file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 document saved to '{outputPath}'.");
    }
}