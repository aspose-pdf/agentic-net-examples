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
        // Input PDF, output PDF and the regex pattern for bookmark titles to delete
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string pattern = @"^Draft.*$"; // example: titles starting with "Draft"

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Compile the regular expression once
        Regex regex = new Regex(pattern, RegexOptions.Compiled);

        // Use PdfBookmarkEditor to manipulate bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Extract all bookmarks (including nested ones)
            Bookmarks allBookmarks = editor.ExtractBookmarks();

            // Collect titles that match the pattern
            List<string> titlesToDelete = new List<string>();
            foreach (Bookmark bm in allBookmarks)
            {
                if (!string.IsNullOrEmpty(bm.Title) && regex.IsMatch(bm.Title))
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
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks matching pattern \"{pattern}\" have been removed. Output saved to '{outputPdf}'.");
    }
}