using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string updatedPdfPath = "updated.pdf";      // PDF with new metadata
        const string outputEpubPath = "output.epub";      // resulting EPUB

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // ---------- Set PDF metadata using the PdfFileInfo facade ----------
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
        {
            pdfInfo.Title    = "My EPUB Title";
            pdfInfo.Author   = "Jane Doe";
            pdfInfo.Subject  = "Sample Subject";
            pdfInfo.Keywords = "Aspose, PDF, EPUB, metadata";

            // Save the PDF with the updated information to a temporary file
            pdfInfo.SaveNewInfo(updatedPdfPath);
        }

        // ---------- Convert the updated PDF to EPUB ----------
        using (Document pdfDoc = new Document(updatedPdfPath))
        {
            // EpubSaveOptions allows setting the EPUB title (optional, already set in PDF)
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                Title = "My EPUB Title"
            };

            pdfDoc.Save(outputEpubPath, epubOptions);
        }

        Console.WriteLine($"EPUB created at '{outputEpubPath}' with title, author, subject, and keywords.");
    }
}