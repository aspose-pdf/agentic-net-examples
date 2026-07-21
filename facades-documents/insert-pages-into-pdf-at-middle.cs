using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the destination PDF, source PDF and the resulting PDF
        const string destinationPath = "destination.pdf";
        const string sourcePath      = "source.pdf";
        const string outputPath      = "merged.pdf";

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

        // Determine the middle insertion position of the destination PDF.
        // Pages are 1‑based in Aspose.Pdf, so we add 1 after integer division.
        int insertPosition;
        using (Document destDoc = new Document(destinationPath))
        {
            insertPosition = (destDoc.Pages.Count / 2) + 1;
        }

        // Define the range of pages from the source PDF to insert.
        // Here we insert pages 1 through 3; adjust as needed.
        int sourceStartPage = 1;
        int sourceEndPage   = 3;

        // Perform the insertion using PdfFileEditor.
        // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Insert(
            destinationPath,   // inputFile (the PDF into which pages will be inserted)
            insertPosition,    // insertLocation (position in the destination PDF)
            sourcePath,        // portFile (the PDF providing pages to insert)
            sourceStartPage,   // startPage (first page from source to insert)
            sourceEndPage,     // endPage   (last page from source to insert)
            outputPath);       // outputFile (resulting PDF)

        if (success)
        {
            Console.WriteLine($"Pages {sourceStartPage}-{sourceEndPage} from '{sourcePath}' " +
                              $"were inserted into '{destinationPath}' at position {insertPosition}.");
            Console.WriteLine($"Result saved as '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Insertion failed.");
        }
    }
}