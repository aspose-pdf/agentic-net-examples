using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;
using System.Collections;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string pattern = @"^Draft.*$"; // adjust the regex as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the bookmark editor
        var editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all bookmarks. In recent Aspose.Pdf versions this returns a Bookmarks collection.
        var bookmarks = editor.ExtractBookmarks();

        // Prepare the regex
        var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Iterate through the collection and delete matching titles.
        // The concrete type of each item may vary between library versions, so we use reflection to obtain the Title.
        foreach (var bm in (IEnumerable)bookmarks)
        {
            var titleProp = bm.GetType().GetProperty("Title");
            var title = titleProp?.GetValue(bm) as string;
            if (!string.IsNullOrEmpty(title) && regex.IsMatch(title))
            {
                // DeleteBookmarks removes all entries that match the supplied title.
                editor.DeleteBookmarks(title);
            }
        }

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Removed bookmarks matching '{pattern}'. Saved to '{outputPath}'.");
    }
}
