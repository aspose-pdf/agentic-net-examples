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
            // Dictionary to map annotation ID (Name or FullName) to its page number
            var annotationMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Prefer the explicit Name; fall back to FullName if Name is empty
                    string id = !string.IsNullOrEmpty(ann.Name) ? ann.Name : ann.FullName;
                    if (string.IsNullOrEmpty(id))
                        continue; // Skip annotations without an identifier

                    // Annotation.PageIndex also returns the 1‑based page number
                    int pageNumber = ann.PageIndex;

                    // Store or update the mapping
                    annotationMap[id] = pageNumber;
                }
            }

            // Example usage: print the mapping
            foreach (var kvp in annotationMap)
            {
                Console.WriteLine($"Annotation ID: {kvp.Key} => Page: {kvp.Value}");
            }

            // The dictionary 'annotationMap' can now be used for fast look‑ups
        }
    }
}