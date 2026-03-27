using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        // Load the PDF document (required for binding to the facade)
        using (Document doc = new Document(inputPath))
        {
            // Extract bookmarks using PdfBookmarkEditor
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(doc);
                Bookmarks bookmarks = editor.ExtractBookmarks();

                // Hard‑coded expected Table of Contents (title → expected page number)
                Dictionary<string, int> expectedToc = new Dictionary<string, int>();
                expectedToc.Add("Introduction", 1);
                expectedToc.Add("Chapter 1", 3);
                expectedToc.Add("Chapter 2", 7);
                expectedToc.Add("Conclusion", 12);

                Console.WriteLine("Bookmark validation results:");
                foreach (Bookmark bm in bookmarks)
                {
                    string title = bm.Title;
                    int pageNumber = bm.PageNumber;
                    if (expectedToc.ContainsKey(title))
                    {
                        int expectedPage = expectedToc[title];
                        if (pageNumber == expectedPage)
                        {
                            Console.WriteLine($"[OK] \"{title}\" is on page {pageNumber} as expected.");
                        }
                        else
                        {
                            Console.WriteLine($"[MISMATCH] \"{title}\" is on page {pageNumber} but expected page {expectedPage}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[UNKNOWN] Bookmark \"{title}\" not found in expected TOC.");
                    }
                }
            }
        }
    }
}
