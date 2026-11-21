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

        // Load the PDF and read existing bookmarks using the correct Facade API
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);
        // The Facade provides ExtractBookmarks() – not GetBookmarks()
        Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

        Console.WriteLine("Existing bookmarks:");
        foreach (var bm in bookmarks)
        {
            // Use dynamic to avoid compile‑time dependency on BookmarkInfo type
            dynamic b = bm;
            Console.WriteLine($"Id: {b.Id}, Title: {b.Title}");
        }

        if (bookmarks == null || bookmarks.Count == 0)
        {
            Console.WriteLine("No bookmarks to delete.");
            bookmarkEditor.Close();
            return;
        }

        // Choose a bookmark identifier to delete (here we take the first one)
        string idToDelete = ((dynamic)bookmarks[0]).Id;
        Console.WriteLine($"\nDeleting bookmark with Id: {idToDelete}");

        // Delete the selected bookmark – DeleteBookmarks expects a single string, not an array
        bookmarkEditor.DeleteBookmarks(idToDelete);

        // Save the modified PDF to a new file
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        // Verify removal by reloading bookmarks from the saved file
        PdfBookmarkEditor verifyEditor = new PdfBookmarkEditor();
        verifyEditor.BindPdf(outputPath);
        Bookmarks afterDeletion = verifyEditor.ExtractBookmarks();

        Console.WriteLine("\nBookmarks after deletion:");
        foreach (var bm in afterDeletion)
        {
            dynamic b = bm;
            Console.WriteLine($"Id: {b.Id}, Title: {b.Title}");
        }

        bool removed = true;
        foreach (var bm in afterDeletion)
        {
            if (((dynamic)bm).Id == idToDelete)
            {
                removed = false;
                break;
            }
        }
        Console.WriteLine(removed
            ? "\nBookmark successfully removed."
            : "\nBookmark still present.");

        verifyEditor.Close();
    }
}
