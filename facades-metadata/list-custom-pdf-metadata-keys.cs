using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the document's information (metadata)
            DocumentInfo info = doc.Info;

            // DocumentInfo implements IDictionary<string,string> – use its Keys collection
            // to obtain all metadata entries (standard and custom). If you need only custom
            // entries you can filter out the known standard keys.
            IEnumerable<string> allKeys = info.Keys;

            // Sort the keys alphabetically (case‑insensitive)
            string[] sortedKeys = allKeys
                .OrderBy(k => k, StringComparer.OrdinalIgnoreCase)
                .ToArray();

            // Display the sorted keys and their values
            Console.WriteLine("Metadata keys (alphabetical order):");
            foreach (string key in sortedKeys)
            {
                // Retrieve the value via the indexer
                string value = info[key];
                Console.WriteLine($"{key}: {value}");
            }
        }
    }
}
