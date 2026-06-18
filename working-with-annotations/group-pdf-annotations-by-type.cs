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

        // Dictionary to group annotations by their AnnotationType
        var groups = new Dictionary<AnnotationType, List<Annotation>>();

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annotations = page.Annotations;

                // Iterate over each annotation on the page
                foreach (Annotation annotation in annotations)
                {
                    AnnotationType type = annotation.AnnotationType;

                    // Create a list for the type if it does not exist yet
                    if (!groups.TryGetValue(type, out List<Annotation> list))
                    {
                        list = new List<Annotation>();
                        groups[type] = list;
                    }

                    list.Add(annotation);
                }
            }
        }

        // Simple reporting: print the count of each annotation type
        Console.WriteLine("Annotation report:");
        foreach (var kvp in groups)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value.Count} instance(s)");
        }
    }
}
