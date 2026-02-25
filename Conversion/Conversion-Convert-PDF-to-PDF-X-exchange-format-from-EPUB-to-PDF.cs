using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input EPUB file and desired PDF/X output paths
        const string epubPath      = "input.epub";
        const string pdfxPath      = "output_pdfx3.pdf";
        const string conversionLog = "conversion_log.txt";

        // Verify the source file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"Source file not found: {epubPath}");
            return;
        }

        try
        {
            // Load the EPUB document (conversion to PDF happens internally)
            using (Document doc = new Document(epubPath, new EpubLoadOptions()))
            {
                // Convert the in‑memory PDF to PDF/X‑3 format.
                // Errors (if any) are written to the specified log file.
                doc.Convert(conversionLog, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the resulting PDF/X document.
                doc.Save(pdfxPath);
            }

            Console.WriteLine($"EPUB successfully converted to PDF/X → '{pdfxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}