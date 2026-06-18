using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, title of the bookmark to delete, and output PDF paths
        const string inputPdf   = "input.pdf";
        const string bookmarkToDelete = "Chapter 2"; // title of the bookmark to remove
        const string outputPdf  = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor (facade) to work with bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // ------------------------------------------------------------
            // Extract and display all bookmarks before deletion (verification)
            // ------------------------------------------------------------
            Console.WriteLine("Bookmarks before deletion:");
            Bookmarks before = editor.ExtractBookmarks(); // extracts all levels
            foreach (Bookmark bm in before)
            {
                Console.WriteLine($"- {bm.Title}");
            }

            // ------------------------------------------------------------
            // Delete the bookmark with the specified title
            // ------------------------------------------------------------
            editor.DeleteBookmarks(bookmarkToDelete);

            // ------------------------------------------------------------
            // Extract and display all bookmarks after deletion (verification)
            // ------------------------------------------------------------
            Console.WriteLine("\nBookmarks after deletion:");
            Bookmarks after = editor.ExtractBookmarks();
            foreach (Bookmark bm in after)
            {
                Console.WriteLine($"- {bm.Title}");
            }

            // Save the modified PDF to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"\nProcessed PDF saved as '{outputPdf}'.");
    }
}