using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

public class BookmarkHelper
{
    /// <summary>
    /// Retrieves all bookmarks from the specified PDF file.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF document.</param>
    /// <returns>A Bookmarks collection containing all bookmarks (including nested ones).</returns>
    public static Bookmarks GetAllBookmarks(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // PdfBookmarkEditor implements IDisposable via SaveableFacade, so we use a using block.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the editor to the PDF file.
            editor.BindPdf(pdfPath);

            // Extract all bookmarks recursively.
            Bookmarks bookmarks = editor.ExtractBookmarks();

            // Close the editor (optional, as using will dispose it).
            editor.Close();

            return bookmarks;
        }
    }

    // Example usage
    public static void Main()
    {
        const string inputPdf = "sample.pdf";

        try
        {
            Bookmarks allBookmarks = GetAllBookmarks(inputPdf);

            Console.WriteLine($"Total bookmarks found: {allBookmarks.Count}");
            foreach (Bookmark bm in allBookmarks)
            {
                Console.WriteLine($"Title: {bm.Title}, Page: {bm.PageNumber}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}