using System;
using System.IO;
using Aspose.Pdf.Facades;   // Provides PdfBookmarkEditor and Bookmark classes

class PdfBookmarkManager
{
    static void Main(string[] args)
    {
        // Input PDF path
        const string inputPdf = "input.pdf";
        // Output PDF path after modifications
        const string outputPdf = "output.pdf";

        // Titles to work with
        const string titleToModify = "Old Bookmark Title";
        const string newTitle = "Updated Bookmark Title";
        const string titleToDelete = "Bookmark To Delete";

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found at '{inputPdf}'.");
            return;
        }

        try
        {
            // Initialize the bookmark editor and bind the PDF document
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(inputPdf);

                // -------------------- Get all bookmarks --------------------
                Console.WriteLine("Existing bookmarks:");
                var allBookmarks = editor.ExtractBookmarks(); // returns Bookmarks collection
                foreach (Bookmark bm in allBookmarks)
                {
                    PrintBookmark(bm, 0);
                }

                // -------------------- Update a bookmark title --------------------
                // ModifyBookmarks changes the title of the first bookmark that matches the old title
                editor.ModifyBookmarks(titleToModify, newTitle);
                Console.WriteLine($"\nModified bookmark title from '{titleToModify}' to '{newTitle}'.");

                // -------------------- Delete a specific bookmark --------------------
                // DeleteBookmarks removes the bookmark with the exact title
                editor.DeleteBookmarks(titleToDelete);
                Console.WriteLine($"\nDeleted bookmark with title '{titleToDelete}' (if it existed).");

                // -------------------- Save the updated PDF --------------------
                editor.Save(outputPdf);
                Console.WriteLine($"\nUpdated PDF saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Recursive helper to print bookmark hierarchy with indentation
    static void PrintBookmark(Bookmark bookmark, int level)
    {
        string indent = new string(' ', level * 4);
        Console.WriteLine($"{indent}- {bookmark.Title}");

        // If the bookmark has child items, print them recursively
        if (bookmark.ChildItems != null && bookmark.ChildItems.Count > 0)
        {
            foreach (Bookmark child in bookmark.ChildItems)
            {
                PrintBookmark(child, level + 1);
            }
        }
    }
}