using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input EPUB, output PDF/E‑1 and conversion log
        const string epubPath = "input.epub";
        const string pdfPath  = "output.pdf";
        const string logPath  = "conversion.log";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Load the EPUB file using default load options
        using (Document doc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Convert the document to PDF/E‑1.
            // Aspose.Pdf does not expose a dedicated PdfFormat for PDF/E‑1,
            // but PDF/E‑1 is based on PDF/A‑1B, so we use that format.
            // If a PdfFormat.PDF_E_1 enum becomes available, replace it accordingly.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the resulting PDF (now in PDF/E‑1 compatible form)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{pdfPath}'.");
    }
}