using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkId = "MyBookmarkId";   // identifier of the bookmark (title used as ID)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor within a using block for deterministic disposal
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Extract all existing bookmarks
            var allBookmarks = editor.ExtractBookmarks();

            // Locate the bookmark whose Title matches the supplied ID
            Bookmark targetBookmark = null;
            foreach (Bookmark bm in allBookmarks)
            {
                if (bm.Title == bookmarkId)
                {
                    targetBookmark = bm;
                    break;
                }
            }

            if (targetBookmark != null)
            {
                // Update the destination to page 10 (as a string)
                targetBookmark.Destination = "10";

                // Remove the old bookmark entry
                editor.DeleteBookmarks(bookmarkId);

                // Add the modified bookmark back into the document
                editor.CreateBookmarks(targetBookmark);
            }
            else
            {
                Console.WriteLine($"Bookmark with ID '{bookmarkId}' not found.");
            }

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}