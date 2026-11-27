using System;
using System.Drawing;
using System.IO;
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

        // Use PdfBookmarkEditor inside a using block (it implements IDisposable)
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the source PDF
            bookmarkEditor.BindPdf(inputPath);

            // Load the document only to get the page count
            using (Document doc = new Document(inputPath))
            {
                int pageCount = doc.Pages.Count; // 1‑based indexing
                for (int i = 1; i <= pageCount; i++)
                {
                    // Build a Bookmark object with the required properties
                    Bookmark bm = new Bookmark
                    {
                        Title = $"Page {i}",
                        PageNumber = i,
                        BoldFlag = true,
                        // Fully qualify the colour to avoid ambiguity with Aspose.Pdf.Color
                        TitleColor = System.Drawing.Color.Blue
                    };

                    // Add the bookmark to the PDF (single‑bookmark overload)
                    bookmarkEditor.CreateBookmarks(bm);
                }
            }

            // Save the PDF with the newly added bookmarks
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
