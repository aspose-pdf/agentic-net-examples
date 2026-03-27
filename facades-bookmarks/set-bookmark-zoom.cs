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

        using (Document doc = new Document(inputPath))
        {
            // Initialise the bookmark editor with the opened document
            PdfBookmarkEditor editor = new PdfBookmarkEditor(doc);

            // Extract the collection of top‑level bookmarks (Bookmarks implements IEnumerable<Bookmark>)
            Bookmarks bookmarks = editor.ExtractBookmarks();
            if (bookmarks != null && bookmarks.Count > 0)
            {
                foreach (Bookmark bm in bookmarks)
                {
                    SetZoomRecursive(bm);
                }
            }

            // Save the modified PDF (bookmarks now contain the 150% zoom setting)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks updated with 150% zoom saved to '{outputPath}'.");
    }

    private static void SetZoomRecursive(Bookmark bm)
    {
        // 150% zoom – the property expects an integer representing the percentage
        bm.PageDisplay_Zoom = 150;

        // Recursively apply the same zoom to any child bookmarks
        if (bm.ChildItems != null)
        {
            foreach (Bookmark child in bm.ChildItems)
            {
                SetZoomRecursive(child);
            }
        }
    }
}
