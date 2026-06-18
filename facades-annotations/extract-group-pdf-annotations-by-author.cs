using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationReporter
{
    // Extracts annotations from a PDF, groups them by author (Title property), and returns the grouping.
    public static Dictionary<string, List<Annotation>> ExtractAndGroupByAuthor(string pdfPath)
    {
        // Dictionary to hold author -> list of annotations
        var authorGroups = new Dictionary<string, List<Annotation>>(StringComparer.OrdinalIgnoreCase);

        // Bind the PDF using the Facade editor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Determine total pages via the underlying Document
            int totalPages = editor.Document.Pages.Count;

            // Define a broad set of annotation type names to retrieve most common types
            string[] annotTypes = new string[]
            {
                "Text", "Highlight", "Underline", "StrikeOut", "Squiggly", "Caret",
                "Ink", "FileAttachment", "Sound", "Movie", "Screen", "Link",
                "Popup", "FreeText", "Stamp", "RubberStamp", "Polygon", "PolyLine",
                "Line", "Square", "Circle"
            };

            // Extract annotations from all pages
            IList<Annotation> annotations = editor.ExtractAnnotations(1, totalPages, annotTypes);

            // Process each annotation
            foreach (Annotation annot in annotations)
            {
                // The author of a markup annotation is stored in the Title property.
                // Not all annotations have a Title; treat missing titles as "Unknown".
                string author = "Unknown";

                if (annot is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                {
                    author = markup.Title;
                }

                // Add annotation to the appropriate author group
                if (!authorGroups.TryGetValue(author, out List<Annotation> list))
                {
                    list = new List<Annotation>();
                    authorGroups[author] = list;
                }
                list.Add(annot);
            }
        }

        return authorGroups;
    }

    // Example usage: prints a simple report to the console.
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        var groups = ExtractAndGroupByAuthor(inputPdf);

        Console.WriteLine("Annotation Author Report");
        Console.WriteLine("------------------------");
        foreach (var kvp in groups)
        {
            string author = kvp.Key;
            int count = kvp.Value.Count;
            Console.WriteLine($"Author: {author,-30} Count: {count}");
        }
    }
}