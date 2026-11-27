using System;
using System.IO;
using System.Drawing;
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

        // Determine the number of pages in the source PDF.
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Use PdfBookmarkEditor to add bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            for (int i = 1; i <= pageCount; i++)
            {
                // Build a single Bookmark instance with the required style.
                Bookmark bm = new Bookmark
                {
                    Title = $"Page {i}",
                    PageNumber = i,
                    ItalicFlag = true,               // italic style
                    TitleColor = System.Drawing.Color.Gray // fully qualified to avoid ambiguity
                };

                // Add the bookmark to the document.
                editor.CreateBookmarks(bm);
            }

            // Save the PDF with the new bookmarks.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
