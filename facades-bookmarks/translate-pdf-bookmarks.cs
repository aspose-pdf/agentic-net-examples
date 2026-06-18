using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF with existing bookmarks
        const string inputPdf  = "input.pdf";
        // Output PDF where bookmark titles will be translated
        const string outputPdf = "translated_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Translation dictionary: key = original bookmark title, value = translated title
        var translation = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Chapter 1", "Capítulo 1" },
            { "Chapter 2", "Capítulo 2" },
            { "Introduction", "Introducción" },
            // add more entries as needed
        };

        // Use PdfBookmarkEditor to modify bookmarks
        using (var editor = new PdfBookmarkEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPdf);

            // Apply each translation
            foreach (var kvp in translation)
            {
                // ModifyBookmarks changes the title of the bookmark matching kvp.Key to kvp.Value
                editor.ModifyBookmarks(kvp.Key, kvp.Value);
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks translated and saved to '{outputPdf}'.");
    }
}