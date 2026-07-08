using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pagesToInsert = 2; // number of pages to add at the beginning

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Insert empty pages at the very start (1‑based indexing)
            for (int i = 0; i < pagesToInsert; i++)
            {
                doc.Pages.Insert(1);
            }

            // Initialize the bookmark editor and bind it to the in‑memory document
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(doc);

            // Extract current bookmarks
            Bookmarks existingBookmarks = editor.ExtractBookmarks();

            // Remove all existing bookmarks
            editor.DeleteBookmarks();

            // Re‑create each bookmark with its page number shifted forward
            foreach (Bookmark bm in existingBookmarks)
            {
                int newPageNumber = bm.PageNumber + pagesToInsert;
                // Guard against out‑of‑range page numbers
                if (newPageNumber <= doc.Pages.Count && newPageNumber > 0)
                {
                    editor.CreateBookmarkOfPage(bm.Title, newPageNumber);
                }
            }

            // Save the updated PDF (including the adjusted bookmarks)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Adjusted PDF saved to '{outputPath}'.");
    }
}