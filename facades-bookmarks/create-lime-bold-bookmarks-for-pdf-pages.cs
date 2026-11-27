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
        const string outputPath = "output.pdf";

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

        // Use PdfBookmarkEditor to add bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Create a lime‑colored bold bookmark for each page
            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark
                {
                    Title = $"Page {i}",
                    PageNumber = i,
                    TitleColor = System.Drawing.Color.Lime, // fully qualified to avoid ambiguity
                    BoldFlag = true,
                    ItalicFlag = false,
                    Open = true // optional – make the bookmark expanded
                };

                // Add the single bookmark to the document
                editor.CreateBookmarks(bm);
            }

            // Save the PDF with the new bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
