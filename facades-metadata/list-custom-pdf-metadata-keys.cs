using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document. Document implements IDisposable, so wrap it in a using block.
        using (Document doc = new Document(pdfPath))
        {
            // DocumentInfo (doc.Info) implements IDictionary<string,string>.
            // Its Keys collection contains all metadata keys, including custom ones.
            // Filter out the standard PDF metadata keys to keep only custom entries.
            var customKeys = doc.Info.Keys
                .Where(k => !IsStandardMetadata(k))
                .OrderBy(k => k, StringComparer.OrdinalIgnoreCase)
                .ToList();

            Console.WriteLine("Custom metadata keys (alphabetical):");
            foreach (var key in customKeys)
            {
                // Retrieve the value using the indexer.
                string value = doc.Info[key];
                Console.WriteLine($"{key}: {value}");
            }
        }
    }

    // Helper method to identify standard PDF metadata keys.
    static bool IsStandardMetadata(string key)
    {
        string[] standard = {
            "Title", "Author", "Subject", "Keywords",
            "Creator", "Producer", "CreationDate", "ModDate", "Trapped"
        };
        return standard.Contains(key);
    }
}