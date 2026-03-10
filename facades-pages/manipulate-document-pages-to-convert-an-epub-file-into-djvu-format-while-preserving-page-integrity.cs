using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";
        const string pdfPath  = "intermediate.pdf";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Load the EPUB file and convert it to a PDF document.
        // EpubLoadOptions creates a PDF with default A4 page size.
        using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
        {
            // OPTIONAL: Use PdfFileEditor if any page‑level manipulation is required.
            // In this example we simply preserve the pages as they are.
            PdfFileEditor editor = new PdfFileEditor();

            // Save the intermediate PDF. This ensures that the page order and
            // content are retained before any further processing.
            pdfDoc.Save(pdfPath);
        }

        // NOTE:
        // Aspose.Pdf does NOT provide a DJVU output format. Therefore the
        // conversion can stop at PDF. If a DJVU file is required, the generated
        // PDF can be passed to an external tool that supports PDF‑to‑DJVU conversion.

        Console.WriteLine($"EPUB successfully converted to PDF: {pdfPath}");
    }
}