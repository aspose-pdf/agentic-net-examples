using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfx3.pdf";
        const string logPath = "conversion_log.xml";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add three blank pages (Aspose.Pdf uses 1‑based indexing internally)
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Convert the document to PDF/X‑3 compliance
            // The conversion logs any errors to the specified XML file
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X‑3 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 document saved to '{outputPath}'.");
    }
}