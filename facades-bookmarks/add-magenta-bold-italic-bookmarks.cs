using System;
using System.IO;
using System.Drawing;               // for Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a bookmark editor, bind the loaded document and create bookmarks
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(doc);

                // Create a magenta, bold‑italic bookmark for each page.
                // The overload CreateBookmarks(Color, bool boldFlag, bool italicFlag)
                // generates a bookmark per page with the supplied style.
                bookmarkEditor.CreateBookmarks(System.Drawing.Color.Magenta, true, true);

                // Save the PDF with the new bookmarks
                bookmarkEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
