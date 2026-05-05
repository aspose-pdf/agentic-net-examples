using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_colored_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the bookmark editor
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Extract existing bookmarks
            var originalBookmarks = editor.ExtractBookmarks();

            // Prepare a new collection to hold modified bookmarks
            var modifiedBookmarks = new Bookmarks();

            foreach (Bookmark bm in originalBookmarks)
            {
                // Determine color based on title content
                if (bm.Title != null && bm.Title.IndexOf("warning", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    bm.TitleColor = Color.Red; // Red for warnings
                }
                else
                {
                    bm.TitleColor = Color.Green; // Green for informational parts
                }

                // Add the modified bookmark to the new collection
                modifiedBookmarks.Add(bm);
            }

            // Remove all existing bookmarks
            editor.DeleteBookmarks();

            // Re‑create bookmarks with the updated colors
            foreach (Bookmark bm in modifiedBookmarks)
            {
                editor.CreateBookmarks(bm);
            }

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks updated and saved to '{outputPdf}'.");
    }
}