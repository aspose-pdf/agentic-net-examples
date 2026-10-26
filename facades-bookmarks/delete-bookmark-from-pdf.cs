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
        const string bookmarkIdToDelete = "bookmark_3"; // identifier of the bookmark to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Delete the specified bookmark using the Facades API
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);
        // DeleteBookmarks expects a single string identifier, not an array
        editor.DeleteBookmarks(bookmarkIdToDelete);
        editor.Save(outputPath);

        // Verify removal by extracting remaining bookmarks
        using (Document doc = new Document(outputPath))
        {
            Console.WriteLine("Remaining bookmarks after deletion:");
            // Iterate over the root outline collection
            foreach (OutlineItemCollection outline in doc.Outlines)
            {
                // Only Title is available; Aspose.Pdf does not expose an Id property for outlines
                Console.WriteLine($"- Title: {outline.Title}");
            }
        }

        Console.WriteLine($"Bookmark '{bookmarkIdToDelete}' deleted and saved to '{outputPath}'.");
    }
}
