using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Color is required by Bookmark.TitleColor
using Aspose.Pdf.Facades;                // Facade API for bookmark manipulation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the sections that need bookmarks.
        // Set IsWarning = true for warning sections (red), false for informational (green).
        var sections = new[]
        {
            new { Title = "Warning: Critical Issue", Page = 2, IsWarning = true },
            new { Title = "Info: Overview",           Page = 3, IsWarning = false },
            new { Title = "Warning: Compliance",    Page = 5, IsWarning = true },
            new { Title = "Info: Details",          Page = 7, IsWarning = false }
        };

        // Use PdfBookmarkEditor to bind the PDF, add bookmarks, and save.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Create a bookmark for each section with the appropriate color.
            foreach (var sec in sections)
            {
                Bookmark bm = new Bookmark
                {
                    Title      = sec.Title,
                    PageNumber = sec.Page,
                    // TitleColor expects System.Drawing.Color.
                    TitleColor = sec.IsWarning ? Color.Red : Color.Green,
                    BoldFlag   = true   // optional: make the title bold
                };

                // Add the bookmark to the document.
                editor.CreateBookmarks(bm);
            }

            // Save the modified PDF.
            editor.Save(outputPath);
            editor.Close(); // optional, Dispose will also close
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}