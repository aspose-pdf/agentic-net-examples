using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

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

        // Load the document to obtain the page count
        using (Document doc = new Document(inputPath))
        {
            // PdfBookmarkEditor works with the facade Bookmark model
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(doc);

                // Add a bold bookmark for each page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Bookmark bm = new Bookmark
                    {
                        Title = $"Page {i}",
                        PageNumber = i,
                        BoldFlag = true   // make the bookmark bold
                    };

                    // Create the bookmark in the PDF
                    editor.CreateBookmarks(bm);
                }

                // Save the PDF with the new bookmarks
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
