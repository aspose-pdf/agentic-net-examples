using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source EPUB and PDF/A files
        const string epubSourcePath = "source.epub";
        const string pdfaSourcePath = "source_pdfa.pdf";

        // Paths for the resulting PDF files
        const string pdfFromEpubPath = "converted_from_epub.pdf";
        const string pdfFromPdfaPath = "converted_from_pdfa.pdf";

        // -----------------------------------------------------------------
        // Convert EPUB to PDF
        // -----------------------------------------------------------------
        if (!File.Exists(epubSourcePath))
        {
            Console.Error.WriteLine($"EPUB source not found: {epubSourcePath}");
        }
        else
        {
            // Load the EPUB using EpubLoadOptions (default page size A4)
            using (Document epubDoc = new Document(epubSourcePath, new EpubLoadOptions()))
            {
                // Save as a regular PDF
                epubDoc.Save(pdfFromEpubPath);
                Console.WriteLine($"EPUB converted to PDF: {pdfFromEpubPath}");
            }
        }

        // -----------------------------------------------------------------
        // Convert PDF/A to regular PDF
        // -----------------------------------------------------------------
        if (!File.Exists(pdfaSourcePath))
        {
            Console.Error.WriteLine($"PDF/A source not found: {pdfaSourcePath}");
        }
        else
        {
            // Load the PDF/A document (no special load options required)
            using (Document pdfaDoc = new Document(pdfaSourcePath))
            {
                // Saving without specifying a SaveOptions always writes a PDF,
                // regardless of the file extension. This produces a regular PDF.
                pdfaDoc.Save(pdfFromPdfaPath);
                Console.WriteLine($"PDF/A converted to regular PDF: {pdfFromPdfaPath}");
            }
        }
    }
}
