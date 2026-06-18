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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to hold annotation identifier -> page number (1‑based)
            Dictionary<string, int> annotationMap = new Dictionary<string, int>();

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Each page has an Annotations collection
                foreach (Annotation ann in page.Annotations)
                {
                    // Use the annotation's Name if set; otherwise fallback to FullName
                    string id = !string.IsNullOrEmpty(ann.Name) ? ann.Name : ann.FullName;

                    // Ensure we have a non‑empty identifier before adding to the map
                    if (!string.IsNullOrEmpty(id) && !annotationMap.ContainsKey(id))
                    {
                        // Annotation.PageIndex returns the page index (1‑based)
                        annotationMap[id] = ann.PageIndex;
                    }
                }
            }

            // Example usage: print the mapping
            Console.WriteLine("Annotation ID -> Page Number:");
            foreach (var kvp in annotationMap)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }

            // The dictionary can now be used for quick lookup elsewhere
        }
    }
}