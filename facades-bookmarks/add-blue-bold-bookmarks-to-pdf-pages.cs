using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

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

        // Bind the source PDF to the bookmark editor.
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Determine the number of pages.
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Create a blue, bold bookmark for each page.
        for (int i = 1; i <= pageCount; i++)
        {
            Bookmark bm = new Bookmark
            {
                Title = $"Page {i}",
                PageNumber = i,
                // Fully qualify the Color to avoid ambiguity with Aspose.Pdf.Color
                TitleColor = System.Drawing.Color.Blue,
                BoldFlag = true
            };

            // Add the bookmark as a top‑level entry.
            editor.CreateBookmarks(bm);
        }

        // Save the PDF with the new bookmarks.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
