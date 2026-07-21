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

        // Mapping: annotation identifier -> page number (1‑based)
        var annotationMap = new Dictionary<string, int>();

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Prefer the Name property; fall back to FullName if Name is empty
                    string id = !string.IsNullOrEmpty(annotation.Name) ? annotation.Name : annotation.FullName;

                    if (!string.IsNullOrEmpty(id))
                    {
                        // Store the first occurrence of each ID; duplicates are ignored
                        if (!annotationMap.ContainsKey(id))
                        {
                            annotationMap[id] = pageIndex;
                        }
                    }
                }
            }
        }

        // Display the resulting mapping
        foreach (KeyValuePair<string, int> entry in annotationMap)
        {
            Console.WriteLine($"Annotation ID: {entry.Key}, Page Number: {entry.Value}");
        }
    }
}