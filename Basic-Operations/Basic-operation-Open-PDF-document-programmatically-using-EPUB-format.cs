using System;
using System.IO;
using Aspose.Pdf;          // Document, EpubLoadOptions

class Program
{
    static void Main()
    {
        // Directory that contains the source EPUB file.
        string dataDir = "Data";

        // Input EPUB file path.
        string epubPath = Path.Combine(dataDir, "sample.epub");

        // Output PDF file path.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the EPUB file exists before attempting to load it.
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Create load options for EPUB import. Default options are sufficient for most cases.
        EpubLoadOptions loadOptions = new EpubLoadOptions();

        // Load the EPUB file into an Aspose.Pdf Document using the specified options.
        Document pdfDocument = new Document(epubPath, loadOptions);

        // Save the loaded document as a PDF file.
        pdfDocument.Save(pdfPath);
    }
}