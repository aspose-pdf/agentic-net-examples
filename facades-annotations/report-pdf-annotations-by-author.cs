using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationReporter
{
    // Extracts annotations from a PDF, groups them by author (Title property of markup annotations), and writes a simple report to the console.
    public static void ReportAnnotationsByAuthor(string pdfPath)
    {
        // Load the PDF document inside a using block for deterministic disposal (document-disposal-with-using rule).
        using (Document doc = new Document(pdfPath))
        {
            // Initialize the PdfAnnotationEditor facade on the loaded document.
            PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

            // Define the page range (1‑based indexing) covering the whole document.
            int startPage = 1;
            int endPage   = doc.Pages.Count;

            // List of annotation type names to extract. Including the most common types ensures all annotations are retrieved.
            string[] allTypes = new string[]
            {
                "Text", "Highlight", "Square", "Circle", "Ink", "Stamp", "Link",
                "FreeText", "Line", "Polygon", "PolyLine", "Popup", "FileAttachment",
                "Sound", "Movie", "RubberStamp", "Caret", "Screen", "Watermark",
                "3D", "Redact"
            };

            // Extract annotations from the specified page range and types.
            IList<Annotation> annotations = editor.ExtractAnnotations(startPage, endPage, allTypes);

            // Group annotations by the Title property of markup annotations. If the annotation is not a markup type or Title is empty, use "(No Author)".
            var groups = annotations.GroupBy(a =>
            {
                var markup = a as MarkupAnnotation;
                return string.IsNullOrEmpty(markup?.Title) ? "(No Author)" : markup.Title;
            });

            // Helper to determine the page number of an annotation when the Annotation class does not expose a PageNumber property.
            int GetPageNumber(Annotation ann)
            {
                foreach (Page pg in doc.Pages)
                {
                    if (pg.Annotations != null && pg.Annotations.Contains(ann))
                        return pg.Number; // Page.Number is 1‑based.
                }
                return -1; // Unknown page.
            }

            // Output the grouped report.
            foreach (var group in groups)
            {
                Console.WriteLine($"Author: {group.Key} – Total Annotations: {group.Count()}");
                foreach (var ann in group)
                {
                    // AnnotationType is an enum; convert to string for readability.
                    string typeName = ann.AnnotationType.ToString();
                    int pageNum = GetPageNumber(ann);
                    string pageInfo = pageNum > 0 ? pageNum.ToString() : "?";
                    Console.WriteLine($"  Page {pageInfo}: Type={typeName}, Contents=\"{ann.Contents}\"");
                }
                Console.WriteLine(); // Blank line between authors
            }
        }
    }

    // Example entry point.
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        ReportAnnotationsByAuthor(inputPdf);
    }
}
