using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing bookmarks
        const string inputPath = "input.pdf";
        // Output PDF after unwanted bookmarks are removed
        const string outputPath = "output.pdf";
        // Regular expression pattern to match bookmark titles to delete
        const string pattern = @"^Unwanted.*$";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfBookmarkEditor facade
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Extract all bookmarks from the document
            Bookmarks allBookmarks = editor.ExtractBookmarks();

            // Iterate over each bookmark and delete those whose titles match the regex
            foreach (Bookmark bm in allBookmarks)
            {
                if (bm != null && !string.IsNullOrEmpty(bm.Title) && Regex.IsMatch(bm.Title, pattern))
                {
                    // Delete the bookmark with the matching title
                    editor.DeleteBookmarks(bm.Title);
                }
            }

            // Save the modified PDF to the output path
            editor.Save(outputPath);
            // Release resources held by the facade
            editor.Close();
        }

        Console.WriteLine($"Bookmarks matching pattern \"{pattern}\" have been removed. Output saved to '{outputPath}'.");
    }
}