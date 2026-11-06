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
        const string outputPath = "result.pdf";

        // Define the page range to insert (1‑based inclusive) and the insertion point in the destination PDF
        int startPage = 2;   // first page of the range in the source PDF
        int endPage   = 5;   // last page of the range in the source PDF
        int insertAtPage = 3; // insert after this page number in the destination PDF (0 = at the beginning)

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

        // Load both PDFs from streams
        using (FileStream srcStream = File.OpenRead(sourcePath))
        using (FileStream dstStream = File.OpenRead(destinationPath))
        {
            // Aspose.Pdf.Document can be constructed directly from a Stream
            Document sourceDoc = new Document(srcStream);
            Document destDoc   = new Document(dstStream);

            // Validate page numbers
            if (startPage < 1 || endPage > sourceDoc.Pages.Count || startPage > endPage)
            {
                Console.Error.WriteLine("Invalid source page range.");
                return;
            }
            if (insertAtPage < 0 || insertAtPage > destDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid insertion position.");
                return;
            }

            // Insert pages one by one preserving order
            // Aspose.Pdf uses 1‑based indexing for Pages collection.
            // We want to insert after 'insertAtPage', so the first inserted page should go to position insertAtPage + 1.
            int insertionIndex = insertAtPage + 1; // 1‑based index where the first new page will be placed
            for (int p = startPage; p <= endPage; p++)
            {
                // Clone the page from the source document to avoid reference issues
                Page pageToInsert = sourceDoc.Pages[p];
                // Insert a copy of the page into the destination document
                destDoc.Pages.Insert(insertionIndex, pageToInsert);
                insertionIndex++; // subsequent pages go after the previously inserted one
            }

            // Save the resulting PDF to the output file (or to a MemoryStream if needed)
            destDoc.Save(outputPath);
        }

        Console.WriteLine($"Inserted pages {startPage}-{endPage} from '{sourcePath}' into '{destinationPath}' at position {insertAtPage}.");
        Console.WriteLine($"Result saved as '{outputPath}'.");
    }
}
