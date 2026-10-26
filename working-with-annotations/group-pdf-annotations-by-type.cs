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

        // Load PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Group annotations by their concrete type name
            var groups = new Dictionary<string, List<Annotation>>();

            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    string typeName = annot.GetType().Name; // e.g., TextAnnotation, LinkAnnotation
                    if (!groups.ContainsKey(typeName))
                        groups[typeName] = new List<Annotation>();
                    groups[typeName].Add(annot);
                }
            }

            // Simple report: count per annotation type
            Console.WriteLine($"Total pages: {doc.Pages.Count}");
            Console.WriteLine("Annotation summary by type:");
            foreach (var kvp in groups)
            {
                Console.WriteLine($"- {kvp.Key}: {kvp.Value.Count}");
            }

            // Detailed listing (optional)
            foreach (var kvp in groups)
            {
                Console.WriteLine($"\nAnnotations of type {kvp.Key}:");
                int index = 1;
                foreach (Annotation a in kvp.Value)
                {
                    // Common properties: page number, contents, action, etc.
                    string details = $"Page {GetPageNumber(doc, a)}";
                    if (a is TextAnnotation txt)
                        details += $", Contents=\"{txt.Contents}\"";
                    else if (a is LinkAnnotation link && link.Action != null)
                        details += $", Action={link.Action.GetType().Name}";
                    Console.WriteLine($"  {index++}. {details}");
                }
            }
        }
    }

    // Helper to locate the page that contains a given annotation
    static int GetPageNumber(Document doc, Annotation annotation)
    {
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            if (doc.Pages[i].Annotations.Contains(annotation))
                return i;
        }
        return -1; // not found (should not happen)
    }
}