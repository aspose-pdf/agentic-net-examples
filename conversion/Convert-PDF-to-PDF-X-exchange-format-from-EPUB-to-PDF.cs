using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input EPUB file, temporary PDF, final PDF/X-3 file and conversion log.
        const string epubPath = "input.epub";
        const string tempPdfPath = "intermediate.pdf";
        const string pdfxPath = "output_pdfx3.pdf";
        const string logPath = "conversion_log.txt";

        // Verify the EPUB source exists.
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // --------------------------------------------------------------------
        // Step 1: Load the EPUB and save it as a regular PDF.
        // --------------------------------------------------------------------
        EpubLoadOptions epubLoadOptions = new EpubLoadOptions(); // default options
        using (Document pdfDocument = new Document(epubPath, epubLoadOptions))
        {
            // Save the intermediate PDF.
            pdfDocument.Save(tempPdfPath);
        }

        // --------------------------------------------------------------------
        // Step 2: Open the intermediate PDF and convert it to PDF/X‑3.
        // --------------------------------------------------------------------
        using (Document pdfDocument = new Document(tempPdfPath))
        {
            // Convert to PDF/X‑3 format. Errors are written to the log file.
            pdfDocument.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the final PDF/X‑3 file.
            pdfDocument.Save(pdfxPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X‑3 saved to '{pdfxPath}'.");
    }
}