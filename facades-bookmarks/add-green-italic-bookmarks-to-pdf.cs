using System;
using System.IO;
using System.Drawing; // retained for other potential System.Drawing types
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
            pageCount = doc.Pages.Count; // Aspose.Pdf uses 1‑based indexing
        }

        // Create a PdfBookmarkEditor, bind the PDF, add bookmarks, and save.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file.
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark for each page with green color and italic style.
            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark();
                bm.Title = $"Page {i}";
                bm.PageNumber = i;
                // Fully qualify the System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                bm.TitleColor = System.Drawing.Color.FromArgb(0, 255, 0); // pure green
                bm.ItalicFlag = true;   // italic style
                bm.BoldFlag = false;    // not bold

                // Add the bookmark to the editor.
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the PDF with the new bookmarks.
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
