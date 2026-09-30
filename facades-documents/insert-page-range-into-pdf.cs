using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath      = "source.pdf";      // PDF containing pages to insert
        const string destinationPath = "destination.pdf"; // PDF into which pages will be inserted
        const string outputPath      = "merged_output.pdf";

        // Validate input files
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        // Define the page range to copy from the source PDF (inclusive)
        int startPage   = 2; // first page of the range (1‑based)
        int endPage     = 4; // last page of the range (1‑based)
        // Define after which page in the destination PDF the range will be inserted
        int insertAfter = 5; // insert after this page number (1‑based)

        // Load PDFs from streams (no temporary files needed)
        using (FileStream srcStream = File.OpenRead(sourcePath))
        using (FileStream dstStream = File.OpenRead(destinationPath))
        {
            // Load documents
            Document srcDoc = new Document(srcStream);
            Document dstDoc = new Document(dstStream);

            // Ensure the requested range is valid
            if (startPage < 1 || endPage > srcDoc.Pages.Count || startPage > endPage)
            {
                Console.Error.WriteLine("Invalid page range specified for the source PDF.");
                return;
            }
            if (insertAfter < 0 || insertAfter > dstDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid insert position for the destination PDF.");
                return;
            }

            // The position where the first new page will be inserted (1‑based index).
            // Insert after page 'insertAfter' means the new page becomes insertAfter+1.
            int insertPos = insertAfter + 1;

            // Insert each page from the source range into the destination document.
            for (int p = startPage; p <= endPage; p++)
            {
                // The Insert method automatically imports the page into the target document.
                dstDoc.Pages.Insert(insertPos, srcDoc.Pages[p]);
                insertPos++; // subsequent pages should follow the previously inserted one
            }

            // Save the merged document to the desired output location.
            dstDoc.Save(outputPath);
        }

        Console.WriteLine($"Pages {startPage}-{endPage} from '{sourcePath}' inserted after page {insertAfter} of '{destinationPath}'.");
        Console.WriteLine($"Result saved to '{outputPath}'.");
    }
}
