using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, temporary adjusted PDF and output EPUB paths
        const string inputPdfPath   = "input.pdf";
        const string tempPdfPath    = "adjusted.pdf";
        const string outputEpubPath = "output.epub";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Adjust page dimensions using PdfPageEditor (Facades API)
        // -----------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the source PDF
            pageEditor.BindPdf(inputPdfPath);

            // Set a new page size – for example A5. Any PageSize enum value can be used.
            pageEditor.PageSize = PageSize.A5;

            // Apply the changes to the bound document
            pageEditor.ApplyChanges();

            // Save the modified PDF to a temporary file
            pageEditor.Save(tempPdfPath);
        }

        // ---------------------------------------------------------------
        // Step 2: Convert the adjusted PDF to EPUB
        // ---------------------------------------------------------------
        using (Document pdfDoc = new Document(tempPdfPath))
        {
            // Initialize EPUB save options (default options are sufficient)
            EpubSaveOptions epubOptions = new EpubSaveOptions();

            // Save as EPUB
            pdfDoc.Save(outputEpubPath, epubOptions);
        }

        // Optional: clean up the temporary adjusted PDF
        try
        {
            if (File.Exists(tempPdfPath))
                File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"EPUB file created at '{outputEpubPath}'.");
    }
}