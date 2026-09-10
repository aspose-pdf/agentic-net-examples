using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ConvertErrorAction enum

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.xml";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add three blank pages
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Convert the document to PDF/X‑3 compliance
            // The Convert method writes any conversion errors to the specified log file
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the PDF/X‑3 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 document saved to '{outputPath}'.");
    }
}