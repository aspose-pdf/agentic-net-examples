using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (disposal handled by using)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the bookmark editor and bind the loaded document
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
            bookmarkEditor.BindPdf(doc);

            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imageIndex = 0;

                // Iterate over each image on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;
                    string bookmarkName = $"Image {imageIndex} on page {pageNum}";
                    // Create a bookmark that points to the page containing the image
                    bookmarkEditor.CreateBookmarkOfPage(bookmarkName, pageNum);
                }
            }

            // Save the PDF with the newly created bookmarks
            bookmarkEditor.Save(outputPath);
            // Release resources held by the editor
            bookmarkEditor.Close();
        }

        Console.WriteLine($"Bookmarks added. Output saved to '{outputPath}'.");
    }
}