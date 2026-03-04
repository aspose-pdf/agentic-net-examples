using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // Source PDF
        const string resizedPdfPath = "resized.pdf";      // Temporary PDF after size change
        const string outputEpubPath = "output.epub";      // Destination EPUB

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Change page size using PdfPageEditor (Facades API)
        // -----------------------------------------------------------------
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(inputPdfPath);                 // Load PDF into the editor

        // Set the desired output page size (e.g., A5). PageSize enum is in Aspose.Pdf namespace.
        pageEditor.PageSize = PageSize.A5;

        // Apply the changes to all pages
        pageEditor.ApplyChanges();

        // Save the modified PDF to a temporary file
        pageEditor.Save(resizedPdfPath);

        // -----------------------------------------------------------------
        // 2. Convert the resized PDF to EPUB format
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(resizedPdfPath))
        {
            // Configure EPUB save options – use flow content recognition
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            // Save as EPUB; explicit SaveOptions are required for non‑PDF formats
            pdfDoc.Save(outputEpubPath, epubOptions);
        }

        // Clean up the temporary PDF file
        try { File.Delete(resizedPdfPath); } catch { }

        Console.WriteLine($"EPUB file created at: {outputEpubPath}");
    }
}