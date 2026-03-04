using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the target EPUB
        const string pdfPath   = "input.pdf";
        const string epubPath  = "output.epub";

        // Verify the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Retrieve PDF metadata (title, author, subject, keywords) using
        // the Facades class PdfFileInfo.
        // -----------------------------------------------------------------
        PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

        string title   = pdfInfo.Title   ?? string.Empty;
        string author  = pdfInfo.Author  ?? string.Empty;
        string subject = pdfInfo.Subject ?? string.Empty;
        string keywords= pdfInfo.Keywords?? string.Empty;

        Console.WriteLine("PDF Metadata:");
        Console.WriteLine($"  Title   : {title}");
        Console.WriteLine($"  Author  : {author}");
        Console.WriteLine($"  Subject : {subject}");
        Console.WriteLine($"  Keywords: {keywords}");

        // -----------------------------------------------------------------
        // Load the PDF document and convert it to EPUB.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Set EPUB title (optional) to match PDF title
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                Title = title
            };

            // Save as EPUB; explicit SaveOptions are required for non‑PDF formats
            pdfDoc.Save(epubPath, epubOptions);
        }

        Console.WriteLine($"EPUB file created at: {epubPath}");
    }
}