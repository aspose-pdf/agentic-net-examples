using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "renamed_bookmarks.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Translation dictionary: key = existing bookmark title, value = new title
        var translationMap = new Dictionary<string, string>
        {
            { "Chapter 1", "Kapitel 1" },
            { "Introduction", "Einleitung" },
            { "Conclusion", "Fazit" }
            // Add more mappings as needed
        };

        try
        {
            // Initialize the bookmark editor and bind the source PDF
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(inputPdf);

            // Rename each bookmark according to the dictionary
            foreach (var kvp in translationMap)
            {
                // ModifyBookmarks changes all occurrences of the source title to the destination title
                editor.ModifyBookmarks(kvp.Key, kvp.Value);
            }

            // Save the modified PDF
            editor.Save(outputPdf);
            Console.WriteLine($"Bookmarks renamed and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}