using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the PDF whose pages will be inserted,
        // and the resulting PDF.
        const string sourcePdfPath   = "source.pdf";   // original document
        const string insertPdfPath   = "insert.pdf";   // document containing pages to insert
        const string outputPdfPath   = "merged.pdf";

        // Position (1‑based) in the source PDF where the new pages will be inserted.
        // For example, 1 inserts before the first page, 3 inserts after the second page, etc.
        int insertLocation = 2;

        // Define the range of pages from the insert PDF to be inserted.
        // Pages are also 1‑based.
        int startPage = 1;
        int endPage   = 3; // inclusive

        // Validate that the input files exist.
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(insertPdfPath))
        {
            Console.Error.WriteLine($"Insert file not found: {insertPdfPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the specified page range from insertPdfPath into sourcePdfPath.
            // The method preserves the original formatting and order of pages.
            bool success = editor.Insert(
                sourcePdfPath,      // inputFile
                insertLocation,     // position in source where pages will be inserted
                insertPdfPath,      // portFile (pages to insert)
                startPage,          // startPage in portFile
                endPage,            // endPage in portFile
                outputPdfPath);     // outputFile

            if (success)
                Console.WriteLine($"Pages {startPage}-{endPage} from '{insertPdfPath}' inserted at position {insertLocation} into '{sourcePdfPath}'. Result saved as '{outputPdfPath}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}