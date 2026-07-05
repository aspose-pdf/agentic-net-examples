using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string pattern   = @"^Draft.*$"; // example regex pattern

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor to manipulate bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Extract all bookmarks
            Aspose.Pdf.Facades.Bookmarks bookmarks = editor.ExtractBookmarks();

            // Delete bookmarks whose titles match the regex pattern
            foreach (Bookmark bm in bookmarks)
            {
                if (!string.IsNullOrEmpty(bm.Title) && Regex.IsMatch(bm.Title, pattern))
                {
                    editor.DeleteBookmarks(bm.Title);
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks matching pattern '{pattern}' have been removed. Output saved to '{outputPdf}'.");
    }
}