using System;
using System.IO;
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

        try
        {
            // PdfBookmarkEditor implements IDisposable, so wrap it in a using block
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the source PDF file
                bookmarkEditor.BindPdf(inputPath);

                // Determine the number of pages using a Document instance
                using (Document doc = new Document(inputPath))
                {
                    // Aspose.Pdf uses 1‑based page indexing
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        // Create a simple bookmark for each page; no visual style is specified
                        string title = $"Page {i}";
                        bookmarkEditor.CreateBookmarkOfPage(title, i);
                    }
                }

                // Save the PDF with the newly added bookmarks
                bookmarkEditor.Save(outputPath);
            }

            Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
