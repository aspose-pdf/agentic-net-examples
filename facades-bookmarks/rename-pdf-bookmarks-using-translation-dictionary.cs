using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_translated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Dictionary mapping original bookmark titles to their translations
        var translationDict = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "Chapter 1", "Capítulo 1" },
            { "Introduction", "Introducción" },
            // Add additional mappings as needed
        };

        try
        {
            // Use the PdfBookmarkEditor facade to work with bookmarks
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Rename bookmarks according to the translation dictionary
                foreach (var kvp in translationDict)
                {
                    // ModifyBookmarks changes all bookmarks with the source title to the new title
                    editor.ModifyBookmarks(kvp.Key, kvp.Value);
                }

                // Save the updated PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Bookmarks renamed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}