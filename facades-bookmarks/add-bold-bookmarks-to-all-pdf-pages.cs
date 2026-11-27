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

        try
        {
            // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF document.
                editor.BindPdf(inputPath);

                // Get the total number of pages from the underlying Document.
                int pageCount = editor.Document.Pages.Count;

                // Optional: clear any existing bookmarks.
                editor.DeleteBookmarks();

                // Create a bold‑styled bookmark for each page.
                for (int i = 1; i <= pageCount; i++)
                {
                    Bookmark bm = new Bookmark
                    {
                        Title = $"Page {i}",
                        PageNumber = i,
                        BoldFlag = true // make the bookmark title bold
                    };
                    // Add the bookmark to the PDF.
                    editor.CreateBookmarks(bm);
                }

                // Save the modified PDF with the new bookmarks.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
