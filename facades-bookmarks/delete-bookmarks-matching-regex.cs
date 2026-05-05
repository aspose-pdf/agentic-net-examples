using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the regex pattern for bookmark titles to remove
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string pattern    = @"^Draft.*$"; // example: titles starting with "Draft"

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all bookmarks from the document
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Collect titles that match the regular expression
        List<string> titlesToDelete = new List<string>();
        foreach (Bookmark bm in allBookmarks)
        {
            if (!string.IsNullOrEmpty(bm.Title) && Regex.IsMatch(bm.Title, pattern))
            {
                titlesToDelete.Add(bm.Title);
            }
        }

        // Delete each matching bookmark by title
        foreach (string title in titlesToDelete)
        {
            editor.DeleteBookmarks(title);
        }

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Bookmarks matching pattern \"{pattern}\" have been removed.");
        Console.WriteLine($"Result saved to: {outputPath}");
    }
}