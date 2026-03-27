using System;
using System.IO;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the bookmark editor on the loaded document
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(doc);

                int imageCounter = 1;
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    Page page = doc.Pages[pageNumber];
                    foreach (XImage img in page.Resources.Images)
                    {
                        string bookmarkTitle = $"Image {imageCounter} (Page {pageNumber})";
                        bookmarkEditor.CreateBookmarkOfPage(bookmarkTitle, pageNumber);
                        imageCounter++;
                    }
                }

                // Save the PDF with the new bookmarks
                bookmarkEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}