using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files: left pages (1‑10) and right pages (11‑20)
        const string leftPath  = "left_pages.pdf";
        const string rightPath = "right_pages.pdf";
        const string outputPath = "booklet.pdf";

        // Verify source files exist
        if (!File.Exists(leftPath))
        {
            Console.Error.WriteLine($"Left PDF not found: {leftPath}");
            return;
        }
        if (!File.Exists(rightPath))
        {
            Console.Error.WriteLine($"Right PDF not found: {rightPath}");
            return;
        }

        // Load source documents
        using (Document leftDoc = new Document(leftPath))
        using (Document rightDoc = new Document(rightPath))
        using (Document booklet = new Document())
        {
            // Interleave pages: left page i, then right page i (i = 1..10)
            for (int i = 1; i <= 10; i++)
            {
                // Insert left‑hand page
                booklet.Pages.Insert(booklet.Pages.Count + 1, leftDoc.Pages[i]);

                // Insert corresponding right‑hand page directly after the left page
                booklet.Pages.Insert(booklet.Pages.Count + 1, rightDoc.Pages[i]);
            }

            // Save the resulting booklet
            booklet.Save(outputPath);
        }

        Console.WriteLine($"Booklet created successfully: {outputPath}");
    }
}
