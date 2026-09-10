using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string pattern    = @"^Unwanted.*$"; // adjust regex as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all bookmarks
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Collect titles that match the regex
        var regex = new Regex(pattern, RegexOptions.Compiled);
        var titlesToDelete = new System.Collections.Generic.List<string>();

        foreach (Bookmark bm in allBookmarks)
        {
            if (bm.Title != null && regex.IsMatch(bm.Title))
                titlesToDelete.Add(bm.Title);
        }

        // Delete each matching bookmark by title
        foreach (string title in titlesToDelete)
        {
            editor.DeleteBookmarks(title);
        }

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Bookmarks matching pattern '{pattern}' have been removed. Saved to '{outputPath}'.");
    }
}