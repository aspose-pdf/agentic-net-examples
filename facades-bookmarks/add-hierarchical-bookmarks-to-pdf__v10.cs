using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Bookmark, Bookmarks

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

        // PdfBookmarkEditor does not implement IDisposable in older versions, but using it in a
        // using‑statement is safe for newer releases and guarantees Close() is called.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF
            editor.BindPdf(inputPath);

            // ------------------------------------------------------------
            // Build the bookmark hierarchy using the Facade Bookmark model
            // ------------------------------------------------------------
            // Parent bookmark (Section 2) on page 5
            Bookmark parent = new Bookmark
            {
                Title = "Section 2",
                PageNumber = 5,
                // Optional: Open = true; // expands the node when the PDF is opened
            };

            // Initialise the child collection – it is a Bookmarks object
            parent.ChildItems = new Bookmarks();

            // Child 1 – Subsection 2.1 on page 6
            Bookmark child1 = new Bookmark
            {
                Title = "Subsection 2.1",
                PageNumber = 6
            };

            // Child 2 – Subsection 2.2 on page 7
            Bookmark child2 = new Bookmark
            {
                Title = "Subsection 2.2",
                PageNumber = 7
            };

            // Add children to the parent
            parent.ChildItems.Add(child1);
            parent.ChildItems.Add(child2);

            // Create the bookmarks in the PDF – CreateBookmarks accepts a single Bookmark
            // and will also write any ChildItems it contains.
            editor.CreateBookmarks(parent);

            // Save the modified PDF
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
