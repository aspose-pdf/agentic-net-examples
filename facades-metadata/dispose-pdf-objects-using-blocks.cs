using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, Page, etc.)
using Aspose.Pdf.Facades;      // Facade API (PdfFileInfo, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo is a facade that provides information about a PDF.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            // Example: read the number of pages from the PDF.
            int pageCount = fileInfo.NumberOfPages;
            Console.WriteLine($"Source PDF contains {pageCount} page(s).");

            // Document is the core object representing a PDF.
            // It also implements IDisposable; use a nested using block.
            using (Document doc = new Document(inputPath))
            {
                // Example operation: add a blank page at the end of the document.
                doc.Pages.Add();

                // Save the modified document to a new file.
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            } // Document is disposed here (resources released).
        } // PdfFileInfo is disposed here (resources released).
    }
}