using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class AnnotationReporter
{
    // Holds annotation together with its page number for reporting
    private class AnnotationInfo
    {
        public int PageNumber { get; set; }
        public Annotation Annotation { get; set; }
    }

    /// <summary>
    /// Extracts all annotations from the PDF, groups them by author (Title property of MarkupAnnotation),
    /// and writes a simple report to the console.
    /// </summary>
    /// <param name="pdfPath">Path to the input PDF file.</param>
    public static void ExtractAndReportByAuthor(string pdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Dictionary: author -> list of annotations (with page info)
        var authorGroups = new Dictionary<string, List<AnnotationInfo>>(StringComparer.OrdinalIgnoreCase);

        // Load the document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Aspose.Pdf.Page page = doc.Pages[pageIdx];

                // If the page has no annotations, skip
                if (page.Annotations == null || page.Annotations.Count == 0)
                    continue;

                foreach (Aspose.Pdf.Annotations.Annotation ann in page.Annotations)
                {
                    // Default author when Title is not available
                    string author = "Unknown";

                    // Title belongs to MarkupAnnotation, not the base Annotation class
                    if (ann is Aspose.Pdf.Annotations.MarkupAnnotation markup)
                    {
                        author = !string.IsNullOrEmpty(markup.Title) ? markup.Title : author;
                    }

                    if (!authorGroups.TryGetValue(author, out var list))
                    {
                        list = new List<AnnotationInfo>();
                        authorGroups[author] = list;
                    }

                    list.Add(new AnnotationInfo
                    {
                        PageNumber = pageIdx,
                        Annotation = ann
                    });
                }
            }
        }

        // Reporting
        Console.WriteLine("Annotation report grouped by author:");
        foreach (var kvp in authorGroups)
        {
            string author = kvp.Key;
            List<AnnotationInfo> infos = kvp.Value;

            Console.WriteLine($"Author: {author} – Total Annotations: {infos.Count}");
            foreach (var info in infos)
            {
                // Contents is available on the base Annotation class
                string contents = info.Annotation.Contents ?? string.Empty;
                Console.WriteLine($"  Page {info.PageNumber}: Type={info.Annotation.GetType().Name}, Contents=\"{contents}\"");
            }
        }
    }

    // Example entry point
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: AnnotationReporter <pdf-file-path>");
            return;
        }

        ExtractAndReportByAuthor(args[0]);
    }
}