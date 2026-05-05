using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to inspect its pages and images.
        using (Document doc = new Document(inputPath))
        // Use PdfBookmarkEditor (facade) to create and save bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the same PDF file to the bookmark editor.
            editor.BindPdf(inputPath);

            int bookmarkCounter = 1;

            // Iterate through all pages (1‑based indexing).
            foreach (Page page in doc.Pages)
            {
                int pageNumber = page.Number; // current page index

                int imageCounter = 1;
                // Iterate over the image resources on the page.
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a descriptive bookmark name.
                    string bookmarkName = $"Image {bookmarkCounter} (Page {pageNumber}, Image {imageCounter})";

                    // Create a bookmark that points to the page containing the image.
                    editor.CreateBookmarkOfPage(bookmarkName, pageNumber);

                    bookmarkCounter++;
                    imageCounter++;
                }
            }

            // Save the PDF with the newly added bookmarks.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added. Output saved to '{outputPath}'.");
    }
}