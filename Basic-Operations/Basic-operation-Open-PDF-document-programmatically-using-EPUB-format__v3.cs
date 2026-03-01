using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source EPUB file
        const string epubPath = "input.epub";
        // Path where the resulting PDF will be saved
        const string pdfPath = "output.pdf";

        // Verify that the EPUB file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"File not found: {epubPath}");
            return;
        }

        // Create load options for EPUB input
        EpubLoadOptions loadOptions = new EpubLoadOptions();

        // Load the EPUB file into a Document instance using the load options
        using (Document doc = new Document(epubPath, loadOptions))
        {
            // Save the loaded document as PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"EPUB successfully converted to PDF: {pdfPath}");
    }
}