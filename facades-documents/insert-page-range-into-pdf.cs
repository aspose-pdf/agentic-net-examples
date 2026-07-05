using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPdf = "dest.pdf";   // PDF into which pages will be inserted
        const string sourcePdf      = "source.pdf"; // PDF providing the pages to insert
        const string outputPdf      = "merged.pdf"; // Resulting PDF after insertion

        // Insert configuration
        int insertAtPage = 2; // Position in destination where pages will be inserted (1‑based)
        int startPage    = 3; // First page to take from source (inclusive, 1‑based)
        int endPage      = 5; // Last page to take from source (inclusive, 1‑based)

        // Verify that the input files exist
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so a plain instance is sufficient
            PdfFileEditor editor = new PdfFileEditor();

            // Perform the insertion:
            // Insert pages startPage..endPage from sourcePdf into destinationPdf at insertAtPage,
            // writing the result to outputPdf.
            bool result = editor.Insert(
                destinationPdf,   // inputFile
                insertAtPage,     // insertLocation
                sourcePdf,        // portFile (the PDF providing pages)
                startPage,        // startPage in portFile
                endPage,          // endPage in portFile
                outputPdf);       // outputFile

            if (result)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}