using System;
using System.IO;
using System.Drawing; // keep for other drawing needs if any
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

        // Obtain the total number of pages using a Document (wrapped in using for proper disposal)
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count; // 1‑based page count
        }

        // PdfBookmarkEditor does not implement IDisposable, so we just instantiate it
        var editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Define the purple color – fully qualified to avoid ambiguity with Aspose.Pdf.Color
        System.Drawing.Color purple = System.Drawing.Color.FromArgb(128, 0, 128); // RGB 0.5,0,0.5

        // Create a bookmark for each page using the facade Bookmark model
        for (int i = 1; i <= pageCount; i++)
        {
            var bm = new Bookmark
            {
                Title = $"Page {i}",
                PageNumber = i,
                TitleColor = purple,
                BoldFlag = true,
                ItalicFlag = true
            };

            // The facade accepts a single Bookmark per call
            editor.CreateBookmarks(bm);
        }

        // Save the PDF with the new bookmarks and close the editor
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
