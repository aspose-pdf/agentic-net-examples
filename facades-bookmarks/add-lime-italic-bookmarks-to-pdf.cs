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

        // Determine the number of pages in the source PDF
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // PdfBookmarkEditor does NOT implement IDisposable in the current library version
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Bind the PDF file to the editor
        editor.BindPdf(inputPath);

        // Create a lime‑colored italic bookmark for each page
        for (int i = 1; i <= pageCount; i++)
        {
            Bookmark bm = new Bookmark
            {
                Title = $"Page {i}",
                PageNumber = i,
                TitleColor = System.Drawing.Color.Lime,   // fully qualified to avoid ambiguity
                ItalicFlag = true,
                BoldFlag = false
            };
            // Add the bookmark to the document via the facade API
            editor.CreateBookmarks(bm);
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
