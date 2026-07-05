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
            var annotationMap = new Dictionary<string, int>();

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Iterate through annotations on the current page
                foreach (Annotation annot in page.Annotations)
                {
                    // Use the annotation Name as its identifier if set; otherwise fallback to FullName
                    string id = !string.IsNullOrEmpty(annot.Name) ? annot.Name : annot.FullName;

                    // Ensure we have a non‑empty identifier before adding to the map
                    if (!string.IsNullOrEmpty(id))
                    {
                        // Annotation.PageIndex returns the page index (1‑based)
                        annotationMap[id] = annot.PageIndex;
                    }
                }
            }

            // Example usage: print the mapping
            Console.WriteLine("Annotation ID -> Page Number");
            foreach (var kvp in annotationMap)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }

            // The dictionary can now be used for quick lookup elsewhere
        }
    }
}