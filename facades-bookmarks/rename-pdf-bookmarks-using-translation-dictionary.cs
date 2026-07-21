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
            { "Chapter 2", "Capítulo 2" },
            { "Conclusion", "Conclusión" }
            // Add more mappings as needed
        };

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Rename each bookmark according to the dictionary
        foreach (var kvp in translationMap)
        {
            // ModifyBookmarks changes all bookmarks with the source title to the destination title
            editor.ModifyBookmarks(kvp.Key, kvp.Value);
        }

        // Save the modified PDF and release resources
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Bookmarks renamed and saved to '{outputPdf}'.");
    }
}