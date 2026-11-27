using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
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

        // Load the PDF to obtain the page count (optional, used only if custom titles are needed)
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Create the bookmark editor and bind it to the source PDF
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(inputPath);

                // Create navy coloured, bold and italic bookmarks for each page
                // This overload automatically creates a top‑level bookmark for every page.
                bookmarkEditor.CreateBookmarks(System.Drawing.Color.Navy, true, true);

                // If you need custom titles per page you can use the following loop instead:
                // for (int i = 1; i <= pageCount; i++)
                // {
                //     var bm = new Bookmark
                //     {
                //         Title      = $"Page {i}",
                //         PageNumber = i,
                //         BoldFlag   = true,
                //         ItalicFlag = true,
                //         TitleColor = System.Drawing.Color.Navy
                //     };
                //     bookmarkEditor.CreateBookmarks(bm);
                // }

                // Save the PDF with the new bookmarks
                bookmarkEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
