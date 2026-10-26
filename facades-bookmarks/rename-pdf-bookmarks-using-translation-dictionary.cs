using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Recursively rename bookmarks (outline items) using the provided translation dictionary
    static void RenameBookmarks(IEnumerable<OutlineItemCollection> items, Dictionary<string, string> translations)
    {
        foreach (OutlineItemCollection item in items)
        {
            if (!string.IsNullOrEmpty(item.Title) && translations.TryGetValue(item.Title, out string newTitle))
            {
                item.Title = newTitle; // replace with translated title
            }

            // Process child outline items, if any
            if (item.Count > 0)
            {
                // OutlineItemCollection implements IEnumerable<OutlineItemCollection>,
                // so we can pass it directly to the same method.
                RenameBookmarks(item, translations);
            }
        }
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_translated_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Example translation dictionary: original bookmark title -> translated title
        var translationDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Chapter 1", "Capítulo 1" },
            { "Introduction", "Introducción" },
            { "Conclusion", "Conclusión" }
            // Add more entries as needed
        };

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Rename bookmarks according to the translation dictionary
            RenameBookmarks(pdfDocument.Outlines, translationDict);

            // Save the updated PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Bookmarks renamed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
