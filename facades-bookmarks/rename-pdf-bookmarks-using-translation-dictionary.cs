using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_renamed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Translation dictionary: original bookmark title -> translated title
        var translations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Chapter 1", "Capítulo 1" },
            { "Introduction", "Introducción" },
            // Add more entries as needed
        };

        // Use the Facade API to rename bookmarks
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the source PDF file
            bookmarkEditor.BindPdf(inputPath);

            // Rename each bookmark according to the dictionary
            foreach (var kvp in translations)
            {
                // ModifyBookmarks(sourceTitle, destinationTitle) renames a bookmark if it exists
                bookmarkEditor.ModifyBookmarks(kvp.Key, kvp.Value);
            }

            // Save the updated PDF to a new file
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks renamed and saved to '{outputPath}'.");
    }
}
