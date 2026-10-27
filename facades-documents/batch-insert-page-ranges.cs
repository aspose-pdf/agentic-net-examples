using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Destination PDF path
        const string destPath = "merged_output.pdf";

        // Create an empty destination PDF document (in memory)
        Document destDoc = new Document();

        // Position where the next page will be inserted (1‑based indexing)
        int insertPos = 1;

        // Define source PDFs and the page ranges to insert (inclusive)
        var sources = new[]
        {
            new { Path = "source1.pdf", Start = 1, End = 3 },
            new { Path = "source2.pdf", Start = 2, End = 5 },
            // Add additional sources as needed
        };

        foreach (var src in sources)
        {
            if (!File.Exists(src.Path))
            {
                Console.Error.WriteLine($"Source file not found: {src.Path}");
                continue;
            }

            // Open source PDF to validate page range
            using (Document srcDoc = new Document(src.Path))
            {
                int srcPageCount = srcDoc.Pages.Count;

                // Clamp the requested range to the actual page count
                int startPage = Math.Max(1, src.Start);
                int endPage   = Math.Min(srcPageCount, src.End);

                // Insert each page from the range into the destination PDF
                for (int pageNum = startPage; pageNum <= endPage; pageNum++)
                {
                    // Insert the page at the current insertion position.
                    // The Insert method automatically imports the page into the destination document.
                    destDoc.Pages.Insert(insertPos, srcDoc.Pages[pageNum]);
                    insertPos++; // Move insertion point forward
                }
            }
        }

        // Save the merged document to disk
        destDoc.Save(destPath);

        // Verify the final document
        using (Document finalDoc = new Document(destPath))
        {
            Console.WriteLine($"Merged PDF created: {destPath}");
            Console.WriteLine($"Total pages: {finalDoc.Pages.Count}");
        }
    }
}
