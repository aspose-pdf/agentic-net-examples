using System;
using System.Collections.Generic;
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

        // Load the PDF to inspect its pages and images
        List<(int pageNumber, int imageIndex)> imageLocations = new List<(int, int)>();
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imgIdx = 0;
                foreach (XImage img in page.Resources.Images)
                {
                    // Record each image's page number and its order on that page
                    imageLocations.Add((pageNum, ++imgIdx));
                }
            }
        }

        // Create bookmarks that point to the pages containing images
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPdf);

        int bookmarkCounter = 0;
        foreach (var (pageNumber, imageIndex) in imageLocations)
        {
            bookmarkCounter++;
            string bookmarkName = $"Image {bookmarkCounter} (Page {pageNumber}, Image #{imageIndex})";
            // Create a bookmark that navigates to the specified page
            bookmarkEditor.CreateBookmarkOfPage(bookmarkName, pageNumber);
        }

        // Save the PDF with the new bookmarks
        bookmarkEditor.Save(outputPdf);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added for {bookmarkCounter} images. Output saved to '{outputPdf}'.");
    }
}