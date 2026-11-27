using System;
using System.IO;
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

        // Load the PDF to obtain the page count.
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // PdfBookmarkEditor does NOT implement IDisposable.
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the PDF file to the editor.
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark for each page with italic style.
            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark
                {
                    Title = $"Page {i}",
                    PageNumber = i,
                    ItalicFlag = true   // set italic style
                };

                // Add the bookmark to the document via the editor.
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the modified PDF with the new bookmarks.
            bookmarkEditor.Save(outputPath);
            bookmarkEditor.Close();
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
