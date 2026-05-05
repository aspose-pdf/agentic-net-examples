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
        const string outputPdf      = "merged.pdf"; // Resulting PDF

        // Insert settings (1‑based page numbers)
        int insertLocation = 2;   // Position in destination where pages will be inserted
        int startPage      = 3;   // First page to take from source
        int endPage        = 5;   // Last page to take from source

        // Verify files exist
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
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the page range from sourcePdf into destinationPdf
            bool result = editor.Insert(
                destinationPdf,   // inputFile
                insertLocation,   // insertLocation (1‑based)
                sourcePdf,        // portFile (the PDF providing pages)
                startPage,        // startPage in sourcePdf
                endPage,          // endPage in sourcePdf
                outputPdf);       // outputFile

            if (result)
                Console.WriteLine($"Successfully inserted pages {startPage}-{endPage} from '{sourcePdf}' into '{destinationPdf}' at position {insertLocation}. Output saved as '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}