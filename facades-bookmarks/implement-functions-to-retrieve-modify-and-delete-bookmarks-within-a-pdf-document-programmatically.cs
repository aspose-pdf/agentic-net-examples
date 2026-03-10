using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    // Retrieves all bookmarks from a PDF and writes their titles and page numbers to the console.
    static void RetrieveBookmarks(string pdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF to the editor.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(pdfPath);

            // Extract all bookmarks (recursive).
            Bookmarks bookmarks = editor.ExtractBookmarks();

            Console.WriteLine($"Bookmarks in '{Path.GetFileName(pdfPath)}':");
            foreach (Bookmark bm in bookmarks)
                PrintBookmark(bm, 0);
        }
    }

    // Helper to print a bookmark and its children with indentation.
    static void PrintBookmark(Bookmark bm, int level)
    {
        string indent = new string(' ', level * 2);
        Console.WriteLine($"{indent}- Title: {bm.Title}, Page: {bm.PageNumber}");
        if (bm.ChildItems != null)
        {
            foreach (Bookmark child in bm.ChildItems)
                PrintBookmark(child, level + 1);
        }
    }

    // Modifies the title of a bookmark identified by its current title.
    static void ModifyBookmark(string pdfPath, string sourceTitle, string newTitle, string outputPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(pdfPath);

            // Modify the bookmark title.
            editor.ModifyBookmarks(sourceTitle, newTitle);

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark '{sourceTitle}' renamed to '{newTitle}'. Saved as '{outputPath}'.");
    }

    // Deletes a specific bookmark by title.
    static void DeleteBookmark(string pdfPath, string titleToDelete, string outputPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(pdfPath);

            // Delete the bookmark with the given title.
            editor.DeleteBookmarks(titleToDelete);

            // Save the result.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark '{titleToDelete}' deleted. Saved as '{outputPath}'.");
    }

    // Deletes all bookmarks from the PDF.
    static void DeleteAllBookmarks(string pdfPath, string outputPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(pdfPath);

            // Remove every bookmark.
            editor.DeleteBookmarks();

            // Persist changes.
            editor.Save(outputPath);
        }

        Console.WriteLine($"All bookmarks removed. Saved as '{outputPath}'.");
    }

    static void Main()
    {
        // Example usage:
        string inputPdf = "sample.pdf";

        // 1. Retrieve and display bookmarks.
        RetrieveBookmarks(inputPdf);

        // 2. Modify a bookmark title.
        ModifyBookmark(inputPdf, "Old Title", "New Title", "modified.pdf");

        // 3. Delete a specific bookmark.
        DeleteBookmark("modified.pdf", "Unwanted Bookmark", "after_deletion.pdf");

        // 4. Delete all bookmarks.
        DeleteAllBookmarks("after_deletion.pdf", "no_bookmarks.pdf");
    }
}