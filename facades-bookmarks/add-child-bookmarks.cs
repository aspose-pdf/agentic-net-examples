using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(document);

            // Extract all existing bookmarks
            Bookmarks allBookmarks = editor.ExtractBookmarks();

            // Find the parent bookmark by title
            Bookmark parentBookmark = null;
            foreach (Bookmark bm in allBookmarks)
            {
                if (bm.Title == "Chapter 1")
                {
                    parentBookmark = bm;
                    break;
                }
            }

            if (parentBookmark == null)
            {
                Console.Error.WriteLine("Parent bookmark 'Chapter 1' not found.");
                editor.Save(outputPath);
                return;
            }

            // Create child bookmarks (sub‑sections)
            Bookmark child1 = new Bookmark();
            child1.Title = "Section 1.1";
            child1.PageNumber = 2;

            Bookmark child2 = new Bookmark();
            child2.Title = "Section 1.2";
            child2.PageNumber = 3;

            // Assemble child collection
            Bookmarks childCollection = new Bookmarks();
            childCollection.Add(child1);
            childCollection.Add(child2);

            // Attach children to the parent
            parentBookmark.ChildItems = childCollection;

            // Re‑create the parent bookmark hierarchy (adds parent with its children)
            editor.CreateBookmarks(parentBookmark);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine("Bookmarks updated and saved to '" + outputPath + "'.");
    }
}
