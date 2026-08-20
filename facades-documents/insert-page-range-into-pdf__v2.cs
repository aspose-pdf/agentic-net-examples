using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDF files (replace with actual file locations)
        const string destinationPath = "destination.pdf"; // PDF to receive inserted pages
        const string sourcePath      = "source.pdf";      // PDF providing pages to insert
        const string outputPath      = "merged.pdf";      // Resulting PDF after insertion

        // Insert parameters
        int insertLocation = 2; // Insert after page 1 (1‑based indexing)
        int startPage      = 3; // First page from source to insert (inclusive)
        int endPage        = 5; // Last page from source to insert (inclusive)

        // Ensure input files exist
        if (!File.Exists(destinationPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or more input files were not found.");
            return;
        }

        // Open streams with proper disposal
        using (FileStream destStream = new FileStream(destinationPath, FileMode.Open, FileAccess.Read))
        using (FileStream srcStream  = new FileStream(sourcePath,      FileMode.Open, FileAccess.Read))
        using (FileStream outStream  = new FileStream(outputPath,     FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does not implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the specified page range from srcStream into destStream at the given location
            bool success = editor.Insert(
                inputStream:    destStream,
                insertLocation: insertLocation,
                portStream:     srcStream,
                startPage:      startPage,
                endPage:        endPage,
                outputStream:   outStream);

            if (success)
                Console.WriteLine($"Pages {startPage}-{endPage} from '{sourcePath}' inserted into '{destinationPath}' at position {insertLocation}. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Insertion failed.");
        }
    }
}