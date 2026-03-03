using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_bookmarked.pdf";
        const string htmlExportPath = "bookmarks.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create and add bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF file into the facade
            editor.BindPdf(inputPath);

            // Add a custom bookmark for page 1
            editor.CreateBookmarkOfPage("First Page", 1);

            // Add default bookmarks for all pages
            editor.CreateBookmarks();

            // Persist changes
            editor.Save(outputPath);
        }

        // Extract, display, and modify bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(outputPath);

            // Retrieve all bookmarks (including nested ones)
            Bookmarks allBookmarks = editor.ExtractBookmarks();

            Console.WriteLine("Current bookmarks:");
            foreach (Bookmark bm in allBookmarks)
            {
                Console.WriteLine($"- Title: {bm.Title}, Page: {bm.PageNumber}");
            }

            // Remove the previously added custom bookmark
            editor.DeleteBookmarks("First Page");

            // Save the document after deletion
            editor.Save(outputPath);
        }

        // Export the final bookmark structure to an HTML file
        PdfBookmarkEditor.ExportBookmarksToHtml(outputPath, htmlExportPath);
        Console.WriteLine($"Bookmarks exported to HTML: {htmlExportPath}");
    }
}