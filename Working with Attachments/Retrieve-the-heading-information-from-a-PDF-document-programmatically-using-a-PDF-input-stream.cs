using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF as a stream
        using (FileStream pdfStream = File.OpenRead(inputPath))
        {
            // Initialize the bookmark editor and bind the stream
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(pdfStream);

            // Extract all bookmarks (headings) from the document
            Aspose.Pdf.Facades.Bookmarks headings = editor.ExtractBookmarks();

            // Iterate through the bookmarks and display title and page number
            foreach (Aspose.Pdf.Facades.Bookmark bm in headings)
            {
                Console.WriteLine($"Title: {bm.Title}, Page: {bm.PageNumber}");
            }

            // Release resources held by the editor
            editor.Close();
        }
    }
}