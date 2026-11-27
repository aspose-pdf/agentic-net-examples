using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the PDFs
        const string sourcePdfPath      = "source.pdf";      // PDF whose page will be inserted
        const string destinationPdfPath = "destination.pdf"; // PDF into which the page will be inserted
        const string outputPdfPath      = "merged_output.pdf";

        // Verify that the input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdfPath}");
            return;
        }

        // Page number at which to insert the source PDF.
        // Aspose.Pdf uses 1‑based indexing, so page 5 means after the fourth page.
        int insertAtPage = 5;

        try
        {
            // Load both documents
            Document destinationDoc = new Document(destinationPdfPath);
            Document sourceDoc      = new Document(sourcePdfPath);

            // Insert all pages from the source document at the specified position in the destination document.
            // The overload: Pages.Insert(int pageNumber, PageCollection pages)
            destinationDoc.Pages.Insert(insertAtPage, sourceDoc.Pages);

            // Save the merged document
            destinationDoc.Save(outputPdfPath);

            Console.WriteLine($"Page(s) from '{sourcePdfPath}' inserted into '{destinationPdfPath}' at page {insertAtPage}. Output saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}
