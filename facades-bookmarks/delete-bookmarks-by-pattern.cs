using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string pattern = "^Unwanted.*$"; // adjust the regex as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        List<string> titlesToDelete = new List<string>();

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);
            Aspose.Pdf.Facades.Bookmarks bookmarks = editor.ExtractBookmarks();

            foreach (Aspose.Pdf.Facades.Bookmark bm in bookmarks)
            {
                if (Regex.IsMatch(bm.Title, pattern))
                {
                    titlesToDelete.Add(bm.Title);
                }
            }

            foreach (string title in titlesToDelete)
            {
                editor.DeleteBookmarks(title);
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks matching pattern removed. Saved to '{outputPath}'.");
    }
}