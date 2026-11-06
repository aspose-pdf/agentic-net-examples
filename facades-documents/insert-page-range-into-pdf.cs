using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (pages to insert) and the destination PDF (where pages will be inserted)
        const string sourcePath = "source.pdf";
        const string destinationPath = "destination.pdf";
        // Output file – we copy the destination first to avoid overwriting the original file
        const string outputPath = "result.pdf";

        // Define the page range to insert (inclusive) and the position after which to insert in the destination
        const int startPage = 2;      // first page of the range in source PDF (1‑based)
        const int endPage   = 5;      // last page of the range in source PDF (1‑based)
        const int insertAfterPage = 3; // insert after this page number in the destination PDF (1‑based)

        // Verify that the input files exist
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

        // Prepare the output file (overwrite if it already exists)
        File.Copy(destinationPath, outputPath, true);

        // Load the PDFs using the Document class (PdfFileEditor does not provide InsertPage)
        Document destDoc = new Document(outputPath);
        Document srcDoc  = new Document(sourcePath);

        // Insert each page from the source range into the destination PDF.
        // After each insertion the target page index shifts, so we calculate the new insert position.
        for (int srcPage = startPage; srcPage <= endPage; srcPage++)
        {
            // Calculate the position in the destination where the current source page will be inserted.
            // The first inserted page goes after 'insertAfterPage', the next one after that, etc.
            int insertPos = insertAfterPage + (srcPage - startPage) + 1; // 1‑based index where the new page will be placed

            // Insert the page from the source document into the destination document.
            // The Insert method automatically imports the page into the target document.
            destDoc.Pages.Insert(insertPos, srcDoc.Pages[srcPage]);
        }

        // Save the modified document to the output path.
        destDoc.Save(outputPath);

        Console.WriteLine($"Inserted pages {startPage}-{endPage} from '{sourcePath}' into '{destinationPath}'.");
        Console.WriteLine($"Result saved as '{outputPath}'.");
    }
}
