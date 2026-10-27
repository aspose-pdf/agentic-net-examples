using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;                 // Document, Page, Annotation
using Aspose.Pdf.Annotations;    // Specific annotation types (e.g., TextAnnotation, MarkupAnnotation)

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        ReportAnnotationsByAuthor(pdfPath);
    }

    /// <summary>
    /// Extracts all annotations from the specified PDF, groups them by the author
    /// and prints a simple report to the console.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    static void ReportAnnotationsByAuthor(string pdfPath)
    {
        // Use the high‑level Document API – it provides access to pages and their annotations.
        using (Document pdfDoc = new Document(pdfPath))
        {
            int pageCount = pdfDoc.Pages.Count; // 1‑based indexing in Aspose.Pdf

            // Dictionary to hold author -> list of (annotation, pageNumber).
            var authorAnnotations = new Dictionary<string, List<(Annotation annotation, int pageNumber)>>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages and collect annotations.
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];
                if (page == null) continue;

                // Each page has an Annotations collection.
                foreach (Annotation annot in page.Annotations)
                {
                    // Most markup‑type annotations expose a Subject property that can be used to store author information.
                    string author = (annot as MarkupAnnotation)?.Subject;
                    if (string.IsNullOrWhiteSpace(author))
                        author = "Unknown";

                    if (!authorAnnotations.TryGetValue(author, out var list))
                    {
                        list = new List<(Annotation, int)>();
                        authorAnnotations[author] = list;
                    }

                    list.Add((annot, pageNumber));
                }
            }

            // Output the grouped report.
            Console.WriteLine("Annotation report grouped by author:");
            Console.WriteLine("------------------------------------");

            foreach (var kvp in authorAnnotations.OrderBy(k => k.Key))
            {
                string author = kvp.Key;
                var annots = kvp.Value;

                Console.WriteLine($"Author: {author}");
                Console.WriteLine($"  Total Annotations: {annots.Count}");

                foreach (var (ann, pageNum) in annots)
                {
                    // Common properties available on the base Annotation class.
                    string type = ann.GetType().Name;
                    // Title is only on MarkupAnnotation (or derived types).
                    string title = (ann as MarkupAnnotation)?.Title ?? string.Empty;
                    Console.WriteLine($"    - Type: {type}, Title: {title}, Page: {pageNum}");
                }

                Console.WriteLine();
            }
        }
    }
}
