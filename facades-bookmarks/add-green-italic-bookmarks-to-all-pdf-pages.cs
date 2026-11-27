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
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Obtain the number of pages in the source PDF.
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Use PdfBookmarkEditor – the only class that can create / delete bookmarks
        // via the facade API.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark
                {
                    Title = $"Page {i}",
                    PageNumber = i,
                    Level = 1,               // top‑level bookmark
                    TitleColor = System.Drawing.Color.Green,
                    ItalicFlag = true,
                    BoldFlag = false,
                    Open = true               // expanded in the outline pane
                };

                // Create a single bookmark – the overload accepts ONE Bookmark instance.
                editor.CreateBookmarks(bm);
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
