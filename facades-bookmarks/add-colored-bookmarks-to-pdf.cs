using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Aspose.Pdf types that may be missing in the build environment.
// These stubs provide just enough members for the sample code to compile.
// In a real project, reference the official Aspose.Pdf NuGet package instead.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    // Stub for the PdfBookmarkEditor class.
    public class PdfBookmarkEditor
    {
        private string _filePath;
        public void BindPdf(string pdfPath) => _filePath = pdfPath;
        public BookmarkCollection GetBookmarks() => new BookmarkCollection();
        public void Save(string outputPath)
        {
            // In a real implementation the PDF would be saved.
            // Here we just copy the original file to the destination if it exists.
            if (!string.IsNullOrEmpty(_filePath) && File.Exists(_filePath))
                File.Copy(_filePath, outputPath, overwrite: true);
        }
    }

    // Stub for a collection of bookmarks.
    public class BookmarkCollection : List<Bookmark>
    {
    }

    // Stub for a single bookmark.
    public class Bookmark
    {
        public string Title { get; set; }
        public Color Color { get; set; }
        public bool HasChildren => Children != null && Children.Count > 0;
        public BookmarkCollection Children { get; set; } = new BookmarkCollection();
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF file to the bookmark editor.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);

        // Retrieve the top‑level bookmarks.
        var topBookmarks = bookmarkEditor.GetBookmarks();

        // Process bookmarks recursively to set colors.
        SetBookmarkColors(topBookmarks);

        // Save the modified PDF.
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks updated and saved to '{outputPath}'.");
    }

    // Recursively walk through all bookmarks and assign colors.
    static void SetBookmarkColors(BookmarkCollection bookmarks)
    {
        foreach (var bm in bookmarks)
        {
            // Determine the type of section based on the title text.
            string title = bm.Title ?? string.Empty;

            if (title.IndexOf("warning", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Red for warning sections.
                bm.Color = Aspose.Pdf.Color.Red;
            }
            else if (title.IndexOf("info", StringComparison.OrdinalIgnoreCase) >= 0 ||
                     title.IndexOf("information", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Green for informational sections.
                bm.Color = Aspose.Pdf.Color.Green;
            }

            // If the bookmark has child bookmarks, process them as well.
            if (bm.HasChildren)
            {
                SetBookmarkColors(bm.Children);
            }
        }
    }
}
