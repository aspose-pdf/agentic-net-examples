using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Remove duplicate bookmarks (identical title and destination)
        var seen = new HashSet<string>(StringComparer.Ordinal);
        // Iterate in reverse order so that removal does not affect the loop index
        for (int i = doc.Outlines.Count - 1; i >= 0; i--)
        {
            OutlineItemCollection item = doc.Outlines[i];
            string title = item.Title ?? string.Empty;
            string destKey = GetDestinationKey(item.Destination);
            string key = title + "|" + destKey;

            if (seen.Contains(key))
            {
                // Use Remove to delete the outline item; Delete expects a string title.
                doc.Outlines.Remove(item);
            }
            else
            {
                seen.Add(key);
            }
        }

        // Save the cleaned PDF
        doc.Save(outputPath);
        Console.WriteLine($"Duplicate bookmarks removed. Saved to '{outputPath}'.");
    }

    // Helper to create a stable string representation of a bookmark destination.
    // We avoid referencing concrete destination types (e.g., PageDestination) that may not exist
    // in older Aspose.Pdf versions. The default ToString() implementation is sufficient for
    // equality comparison because identical destinations produce identical strings.
    private static string GetDestinationKey(object destObj)
    {
        if (destObj == null)
            return "null";

        // Ensure a non‑null string is returned even if ToString() somehow returns null.
        return destObj.ToString() ?? string.Empty;
    }
}
