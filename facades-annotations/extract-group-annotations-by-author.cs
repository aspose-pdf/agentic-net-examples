using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        ExtractAndGroupAnnotationsByAuthor(inputPdf);
    }

    // Extracts all annotations, groups them by the annotation author (Title property) and prints a simple report.
    static void ExtractAndGroupAnnotationsByAuthor(string pdfPath)
    {
        // Create the facade and bind the PDF document.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Get total number of pages from the underlying Document.
            int pageCount = editor.Document.Pages.Count;

            // Retrieve annotations of all supported types.
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));
            IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, allTypes);

            // Group annotations by the author (Title property). Use "Unknown" when Title is null or empty.
            var groups = new Dictionary<string, List<Annotation>>(StringComparer.OrdinalIgnoreCase);
            foreach (Annotation ann in annotations)
            {
                // Title exists only on markup annotations, so cast safely.
                string author = "Unknown";
                if (ann is MarkupAnnotation markup && !string.IsNullOrWhiteSpace(markup.Title))
                {
                    author = markup.Title;
                }

                if (!groups.TryGetValue(author, out List<Annotation> list))
                {
                    list = new List<Annotation>();
                    groups[author] = list;
                }
                list.Add(ann);
            }

            // Output the grouped report.
            Console.WriteLine($"Total annotations found: {annotations.Count}");
            foreach (var kvp in groups)
            {
                string author = kvp.Key;
                List<Annotation> authorAnnotations = kvp.Value;
                Console.WriteLine($"\nAuthor: {author} (Count: {authorAnnotations.Count})");
                foreach (Annotation ann in authorAnnotations)
                {
                    // PageIndex is 1‑based.
                    Console.WriteLine($"  - Page {ann.PageIndex}, Type {ann.AnnotationType}, Contents: \"{ann.Contents}\"");
                }
            }

            // No modifications are made, but if you wanted to save a copy you could uncomment:
            // editor.Save("output_copy.pdf");
        }
    }
}
