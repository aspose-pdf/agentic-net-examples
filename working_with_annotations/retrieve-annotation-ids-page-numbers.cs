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
            // Dictionary to map annotation identifier (Name) to its page number
            Dictionary<string, int> annotationMap = new Dictionary<string, int>();

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Iterate through annotations on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Use the annotation's Name as the identifier; fallback to FullName if Name is empty
                    string id = !string.IsNullOrEmpty(ann.Name) ? ann.Name : ann.FullName;

                    // Annotation.PageIndex also returns the page number (1‑based)
                    int pageNumber = ann.PageIndex;

                    // Add to dictionary (if duplicate IDs exist, the last one wins)
                    annotationMap[id] = pageNumber;
                }
            }

            // Example usage: print the mapping
            Console.WriteLine("Annotation ID -> Page Number mapping:");
            foreach (var kvp in annotationMap)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }
    }
}