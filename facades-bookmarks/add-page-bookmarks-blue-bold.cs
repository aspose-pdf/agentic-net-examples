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

        // Initialize the bookmark editor (does not implement IDisposable)
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the source PDF
        bookmarkEditor.BindPdf(inputPath);

        // Determine the number of pages using a Document (no need for using if we only need the count)
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;
        doc.Dispose();

        // Create a bookmark for each page with blue color and bold style
        for (int i = 1; i <= pageCount; i++)
        {
            Bookmark bm = new Bookmark
            {
                Title = $"Page {i}",
                PageNumber = i,
                // Fully qualify the Color to avoid ambiguity between System.Drawing.Color and Aspose.Pdf.Color
                TitleColor = System.Drawing.Color.Blue,
                BoldFlag = true,
                ItalicFlag = false // optional, default is false
            };
            // Add the bookmark to the document via the editor
            bookmarkEditor.CreateBookmarks(bm);
        }

        // Save the PDF with the newly added bookmarks
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
