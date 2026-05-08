using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Inserts selected pages from a source PDF into a destination PDF at a specified position.
    // All PDFs are handled via streams to avoid file‑locking issues.
    static void Main()
    {
        // Paths to the PDFs – replace with your actual file locations.
        const string destinationPdfPath = "destination.pdf"; // PDF that will receive the pages
        const string sourcePdfPath      = "source.pdf";      // PDF that provides the pages
        const string outputPdfPath      = "merged.pdf";      // Resulting PDF after insertion

        // Position in the destination PDF after which pages will be inserted (1‑based index).
        // For example, insertLocation = 2 will place the new pages after page 2.
        int insertLocation = 2;

        // Array of page numbers (1‑based) to take from the source PDF.
        int[] pagesToInsert = new int[] { 3, 4, 5 };

        // Ensure the input files exist before proceeding.
        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Open the streams with appropriate access modes.
        using (FileStream destStream = new FileStream(destinationPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream srcStream  = new FileStream(sourcePdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outStream  = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does not implement IDisposable, so we instantiate it normally.
            PdfFileEditor editor = new PdfFileEditor();

            // Perform the insertion. The method returns true on success.
            bool success = editor.Insert(
                inputStream:    destStream,
                insertLocation: insertLocation,
                portStream:     srcStream,
                pageNumber:     pagesToInsert,
                outputStream:  outStream);

            if (success)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdfPath}'.");
            else
                Console.Error.WriteLine("Insertion failed. Check the input PDFs and page numbers.");
        }
    }
}