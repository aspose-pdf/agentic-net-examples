using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get page count
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count; // 1‑based indexing
        }

        // Bind PDF and add bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark
                {
                    Title = $"Page {i}",
                    PageNumber = i,
                    TitleColor = System.Drawing.Color.Teal,   // fully qualified to avoid ambiguity
                    BoldFlag = true,
                    ItalicFlag = true
                };

                // Add a single bookmark
                editor.CreateBookmarks(bm);
            }

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
