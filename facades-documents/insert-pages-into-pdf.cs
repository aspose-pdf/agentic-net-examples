using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPath = "dest.pdf";   // PDF into which pages will be inserted
        const string sourcePath      = "source.pdf"; // PDF providing the pages to insert
        const string outputPath      = "merged.pdf"; // Resulting PDF after insertion

        // Insert parameters (1‑based indexing)
        const int insertLocation = 3; // Position in destination where pages will be inserted
        const int startPage      = 2; // First page to take from source
        const int endPage        = 5; // Last page to take from source

        // Validate input files
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly
            PdfFileEditor editor = new PdfFileEditor();

            // Perform the insertion using the string‑based overload
            bool success = editor.Insert(
                destinationPath,   // inputFile (the PDF to receive pages)
                insertLocation,    // insertLocation (1‑based position)
                sourcePath,        // portFile (the PDF providing pages)
                startPage,         // startPage (first page to copy)
                endPage,           // endPage (last page to copy)
                outputPath);       // outputFile (resulting PDF)

            if (success)
                Console.WriteLine($"Inserted pages {startPage}-{endPage} from '{sourcePath}' into '{destinationPath}' at position {insertLocation}. Output saved as '{outputPath}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insert: {ex.Message}");
        }
    }
}