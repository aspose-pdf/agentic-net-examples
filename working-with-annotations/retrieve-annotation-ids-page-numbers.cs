using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to map annotation identifier -> page number
            // The base Annotation class does not expose an Id property; use the Name property instead (rule: avoid-annotation-id-property)
            var annotationPageMap = new Dictionary<string, int>();

            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Use Name as the unique identifier; fall back to empty string if not set
                    string id = annotation.Name ?? string.Empty;

                    // Optionally skip annotations without a name (they cannot be looked up reliably)
                    if (string.IsNullOrEmpty(id))
                        continue;

                    // Add to the dictionary; if duplicate names exist, keep the first occurrence
                    if (!annotationPageMap.ContainsKey(id))
                    {
                        annotationPageMap[id] = pageNum;
                    }
                }
            }

            // Example: output the mapping for verification
            Console.WriteLine("Annotation Name -> Page Number mapping:");
            foreach (var kvp in annotationPageMap)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }
    }
}
