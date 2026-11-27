using System;
using System.IO;
using System.Drawing; // System.Drawing.Color is needed for bookmark colors
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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Create the bookmark editor (it does not implement IDisposable)
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(doc);

        // Create a red, bold, italic bookmark for each page
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Bookmark bm = new Bookmark
            {
                Title = $"Page {i}",
                PageNumber = i,
                // Fully qualify the color to avoid ambiguity with Aspose.Pdf.Color
                TitleColor = System.Drawing.Color.Red,
                BoldFlag = true,
                ItalicFlag = true
            };

            // Add the bookmark to the document via the editor
            bookmarkEditor.CreateBookmarks(bm);
        }

        // Save the modified PDF
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}