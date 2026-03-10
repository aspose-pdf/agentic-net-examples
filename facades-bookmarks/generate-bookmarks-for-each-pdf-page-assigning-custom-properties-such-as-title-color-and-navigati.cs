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

        // Bind the PDF to the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Load the document to obtain the page count
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Create a bookmark for each page with custom properties
            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark();
                bm.Title = $"Page {i}";                     // Custom title
                bm.PageNumber = i;                          // Navigation target
                bm.TitleColor = System.Drawing.Color.DarkBlue; // Custom color
                bm.BoldFlag = true;                         // Bold title
                bm.ItalicFlag = false;                      // Not italic

                // Add the bookmark to the PDF
                editor.CreateBookmarks(bm);
            }
        }

        // Save the modified PDF and release resources
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added. Saved to '{outputPath}'.");
    }
}