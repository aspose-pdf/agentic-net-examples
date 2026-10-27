using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputEpubPath = "output.epub";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create default EPUB save options (no custom settings required)
                EpubSaveOptions epubOptions = new EpubSaveOptions();

                // Save the document as EPUB; explicit options ensure non‑PDF output
                pdfDoc.Save(outputEpubPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: {outputEpubPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}