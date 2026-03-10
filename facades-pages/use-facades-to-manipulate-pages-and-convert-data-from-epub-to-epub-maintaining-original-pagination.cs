using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input EPUB file and output EPUB file paths
        const string inputEpubPath  = "input.epub";
        const string outputEpubPath = "output.epub";

        // Temporary PDF files used during conversion
        const string tempPdfPath        = "temp.pdf";
        const string tempPdfModifiedPath = "temp_modified.pdf";

        // Verify the input file exists
        if (!File.Exists(inputEpubPath))
        {
            Console.Error.WriteLine($"Input EPUB not found: {inputEpubPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the EPUB file and convert it to PDF (preserving pagination)
            // ------------------------------------------------------------
            // EpubLoadOptions creates a PDF with one page per EPUB pagination unit.
            var epubLoadOptions = new EpubLoadOptions();

            using (Document pdfDoc = new Document(inputEpubPath, epubLoadOptions))
            {
                // Save the intermediate PDF to a temporary file
                pdfDoc.Save(tempPdfPath);
            }

            // ------------------------------------------------------------
            // 2. Manipulate PDF pages using the PdfFileEditor facade
            // ------------------------------------------------------------
            // Example manipulation: add 20 points margin to all pages.
            // PdfFileEditor works with file paths (or streams); it does not implement IDisposable.
            var pdfEditor = new PdfFileEditor();

            // Add margins (left, right, top, bottom) in default space units (points)
            // The int[] parameter specifies the page numbers to affect; null means all pages.
            int[] allPages = null; // null applies to all pages
            double leftMargin   = 20;
            double rightMargin  = 20;
            double topMargin    = 20;
            double bottomMargin = 20;

            pdfEditor.AddMargins(
                tempPdfPath,               // input PDF
                tempPdfModifiedPath,       // output PDF with margins
                allPages,                  // apply to all pages
                leftMargin,
                rightMargin,
                topMargin,
                bottomMargin);

            // ------------------------------------------------------------
            // 3. Load the manipulated PDF and convert it back to EPUB
            // ------------------------------------------------------------
            using (Document modifiedPdf = new Document(tempPdfModifiedPath))
            {
                // Configure EPUB save options to keep pagination.
                // Use PdfFlow mode which preserves the original page order.
                var epubSaveOptions = new EpubSaveOptions
                {
                    // The field is named ContentRecognitionMode (not a property)
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.PdfFlow
                };

                // Save the final EPUB file
                modifiedPdf.Save(outputEpubPath, epubSaveOptions);
            }

            // Clean up temporary files (optional)
            File.Delete(tempPdfPath);
            File.Delete(tempPdfModifiedPath);

            Console.WriteLine($"EPUB conversion completed. Output saved to '{outputEpubPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}