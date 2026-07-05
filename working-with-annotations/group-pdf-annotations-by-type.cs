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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to group annotations by their type
            var annotationGroups = new Dictionary<AnnotationType, List<Annotation>>();

            // Iterate over all pages (pages collection is 1‑based, but foreach works)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Get the annotation type (enum)
                    AnnotationType type = annotation.AnnotationType;

                    // Ensure a list exists for this type
                    if (!annotationGroups.ContainsKey(type))
                    {
                        annotationGroups[type] = new List<Annotation>();
                    }

                    // Add the annotation to the appropriate group
                    annotationGroups[type].Add(annotation);
                }
            }

            // Reporting: output count of annotations per type
            Console.WriteLine("Annotation counts by type:");
            foreach (KeyValuePair<AnnotationType, List<Annotation>> kvp in annotationGroups)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value.Count}");
            }
        }
    }
}