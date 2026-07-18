using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Collect all annotations from all pages
            List<Annotation> allAnnotations = new List<Annotation>();
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    allAnnotations.Add(ann);
                }
            }

            // Group annotations by their AnnotationType enum value
            var groups = allAnnotations
                .GroupBy(a => a.AnnotationType)
                .OrderBy(g => g.Key); // optional: sort by enum value

            // Report the grouping
            Console.WriteLine($"Total annotations found: {allAnnotations.Count}");
            foreach (var group in groups)
            {
                // AnnotationType is an enum; use ToString() for readable name
                string typeName = group.Key.ToString();
                int count = group.Count();
                Console.WriteLine($"{typeName}: {count} instance(s)");
            }
        }
    }
}