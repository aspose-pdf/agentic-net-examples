using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF – Document implements IDisposable, so wrap in using
        using (Document doc = new Document(inputPath))
        {
            // Group key: annotation type; value: list of (annotation, page number)
            var groups = new Dictionary<AnnotationType, List<(Annotation ann, int page)>>();

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annotations = page.Annotations;

                foreach (Annotation annotation in annotations)
                {
                    AnnotationType type = annotation.AnnotationType;

                    if (!groups.TryGetValue(type, out var list))
                    {
                        list = new List<(Annotation, int)>();
                        groups[type] = list;
                    }

                    list.Add((annotation, i));
                }
            }

            // Simple console report
            Console.WriteLine("=== Annotation Summary ===");
            foreach (var kvp in groups)
            {
                AnnotationType type = kvp.Key;
                List<(Annotation ann, int page)> items = kvp.Value;

                Console.WriteLine($"{type}: {items.Count} occurrence(s)");
                int idx = 1;
                foreach (var (ann, pageNum) in items)
                {
                    // Show basic details – Contents may be empty for some types
                    string contents = string.IsNullOrEmpty(ann.Contents) ? "(no contents)" : ann.Contents;
                    Console.WriteLine($"  {idx++}. Page {pageNum}, Contents: {contents}");
                }
            }
        }
    }
}