using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "renamed_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Translation dictionary: key = existing bookmark title, value = new title
        var translationMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Chapter 1", "Capítulo 1" },
            { "Introduction", "Introducción" },
            { "Conclusion", "Conclusión" }
            // Add more mappings as needed
        };

        // Use PdfBookmarkEditor to modify bookmarks
        using (var editor = new PdfBookmarkEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdf);

            // Rename each bookmark according to the dictionary
            foreach (var kvp in translationMap)
            {
                string sourceTitle = kvp.Key;
                string newTitle    = kvp.Value;

                // ModifyBookmarks changes the title if the source bookmark exists
                editor.ModifyBookmarks(sourceTitle, newTitle);
            }

            // Save the updated PDF
            editor.Save(outputPdf);
            // Close releases the bound document (optional because of using)
            editor.Close();
        }

        Console.WriteLine($"Bookmarks renamed and saved to '{outputPdf}'.");
    }
}