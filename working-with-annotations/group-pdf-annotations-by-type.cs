using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationReporter
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to group annotations by their AnnotationType
            var groups = new Dictionary<AnnotationType, List<Annotation>>();

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annotations = page.Annotations;

                // Iterate through annotations on the current page
                foreach (Annotation ann in annotations)
                {
                    // Ensure the dictionary has a list for this type
                    if (!groups.TryGetValue(ann.AnnotationType, out List<Annotation> list))
                    {
                        list = new List<Annotation>();
                        groups[ann.AnnotationType] = list;
                    }

                    list.Add(ann);
                }
            }

            // Reporting: output count per annotation type
            Console.WriteLine("Annotation Summary:");
            foreach (KeyValuePair<AnnotationType, List<Annotation>> kvp in groups)
            {
                Console.WriteLine($"- {kvp.Key}: {kvp.Value.Count} instance(s)");
            }

            // Optional: detailed listing per page
            Console.WriteLine();
            Console.WriteLine("Detailed Annotation List:");
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annotations = page.Annotations;

                if (annotations.Count == 0) continue;

                Console.WriteLine($"Page {i}:");
                foreach (Annotation ann in annotations)
                {
                    // Example of accessing common properties
                    string name = string.IsNullOrEmpty(ann.Name) ? "(no name)" : ann.Name;
                    Console.WriteLine($"  - Type: {ann.AnnotationType}, Name: {name}");
                }
            }
        }
    }
}