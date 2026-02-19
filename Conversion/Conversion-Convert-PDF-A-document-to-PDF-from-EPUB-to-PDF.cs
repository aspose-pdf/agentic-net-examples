using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Define input EPUB and output PDF file paths
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string epubPath = Path.Combine(dataDir, "sample.epub");
        string pdfPath  = Path.Combine(dataDir, "output.pdf");

        // Verify that the EPUB file exists before proceeding
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"Error: EPUB file not found at '{epubPath}'.");
            return;
        }

        try
        {
            // Load the EPUB document using the appropriate load options
            EpubLoadOptions loadOptions = new EpubLoadOptions();

            // The EpubLoadOptions class does not expose a RecognitionMode property;
            // the recognition mode is relevant when saving to EPUB, not when loading.
            // For conversion to PDF we simply load the document.

            using (Document pdfDocument = new Document(epubPath, loadOptions))
            {
                // Save the loaded document as a regular PDF file.
                // This uses the standard Document.Save method as required by the lifecycle rules.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"EPUB successfully converted to PDF: '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}