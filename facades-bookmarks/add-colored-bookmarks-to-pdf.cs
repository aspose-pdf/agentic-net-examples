using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF for bookmark manipulation using the facade model
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Extract existing bookmarks (facade model returns a Bookmarks collection)
            Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

            // Apply colors based on bookmark titles
            SetBookmarkColors(bookmarks);

            // Save the updated PDF
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks colored and saved to '{outputPath}'.");
    }

    // Recursively set bookmark colors: red for warnings, green for informational parts
    static void SetBookmarkColors(Bookmarks collection)
    {
        foreach (Bookmark bm in collection)
        {
            if (!string.IsNullOrEmpty(bm.Title) &&
                bm.Title.IndexOf("warning", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Use fully‑qualified System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                bm.TitleColor = System.Drawing.Color.Red; // Red for warnings
            }
            else if (!string.IsNullOrEmpty(bm.Title) &&
                     bm.Title.IndexOf("info", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                bm.TitleColor = System.Drawing.Color.Green; // Green for informational parts
            }

            // Process child bookmarks, if any
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                SetBookmarkColors(bm.ChildItems);
            }
        }
    }
}
