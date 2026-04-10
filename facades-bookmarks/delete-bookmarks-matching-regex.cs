using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the regex pattern for bookmark titles to delete
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string pattern    = @"^Draft.*$"; // example: titles starting with "Draft"

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF file
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all bookmarks (recursive)
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Compile the regular expression once
        Regex regex = new Regex(pattern, RegexOptions.Compiled);

        // Iterate through the collection and delete matching titles
        foreach (Bookmark bm in allBookmarks)
        {
            if (bm.Title != null && regex.IsMatch(bm.Title))
            {
                // DeleteBookmarks(string) removes bookmarks with the specified title
                editor.DeleteBookmarks(bm.Title);
            }
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks matching pattern '{pattern}' have been removed.");
        Console.WriteLine($"Result saved to: {outputPath}");
    }
}