using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf = "sample.pdf";
        const string outputPdf = "sample_updated.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind it to the PDF file
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // -------------------------------------------------
        // 1. Retrieve and display all existing bookmarks
        // -------------------------------------------------
        Console.WriteLine("Existing bookmarks:");
        Bookmarks allBookmarks = editor.ExtractBookmarks(); // extracts all levels
        foreach (Bookmark bm in allBookmarks)
        {
            Console.WriteLine($"- Title: {bm.Title}, Page: {bm.PageNumber}");
        }

        // -------------------------------------------------
        // 2. Update a bookmark title (if it exists)
        // -------------------------------------------------
        string oldTitle = "Old Chapter";
        string newTitle = "New Chapter";
        // ModifyBookmarks changes titles that match the old title
        editor.ModifyBookmarks(oldTitle, newTitle);
        Console.WriteLine($"Attempted to rename bookmark '{oldTitle}' to '{newTitle}'.");

        // -------------------------------------------------
        // 3. Delete a specific bookmark by its title
        // -------------------------------------------------
        string titleToDelete = "Obsolete Section";
        editor.DeleteBookmarks(titleToDelete); // deletes the bookmark with this title, if present
        Console.WriteLine($"Attempted to delete bookmark titled '{titleToDelete}'.");

        // -------------------------------------------------
        // 4. Add a new bookmark that points to page 2
        // -------------------------------------------------
        editor.CreateBookmarkOfPage("Added Bookmark", 2);
        Console.WriteLine("Created a new bookmark for page 2.");

        // -------------------------------------------------
        // 5. Save the modified PDF to a new file
        // -------------------------------------------------
        editor.Save(outputPdf);
        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");

        // Release resources held by the editor
        editor.Close();
    }
}