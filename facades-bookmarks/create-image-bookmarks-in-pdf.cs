using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_image_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the bookmark editor facade
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPdf);

            // Load the same PDF as a Document to inspect its pages and images
            using (Document doc = new Document(inputPdf))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Iterate over each image resource on the page
                    int imageIndex = 0;
                    foreach (XImage img in page.Resources.Images)
                    {
                        imageIndex++;

                        // Create a bookmark name that identifies the image
                        string bookmarkName = $"Image {imageIndex} on page {pageNum}";

                        // Create a bookmark that points to the page containing the image
                        bookmarkEditor.CreateBookmarkOfPage(bookmarkName, pageNum);
                    }
                }
            }

            // Save the PDF with the newly added bookmarks
            bookmarkEditor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks added. Output saved to '{outputPdf}'.");
    }
}