using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;                     // PdfAnnotationEditor, etc.
using Aspose.Pdf.Annotations;                // Annotation, AnnotationType, MarkupAnnotation

class AnnotationReporter
{
    /// <summary>
    /// Extracts annotations from a PDF, groups them by the annotation author (Title property of markup annotations),
    /// and writes a simple text report.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="reportPath">Path where the report will be saved.</param>
    public static void ReportAnnotationsByAuthor(string inputPdfPath, string reportPath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Initialize the facade and bind the PDF document.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Determine the page range (Aspose.Pdf uses 1‑based indexing).
            int startPage = 1;
            int endPage   = editor.Document.Pages.Count;

            // Retrieve all annotation types defined in the enum.
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Extract annotations from the specified page range.
            IList<Annotation> annotations = editor.ExtractAnnotations(startPage, endPage, allTypes);

            // Group annotations by the Title property of markup annotations (author).
            var grouped = annotations
                .GroupBy(a =>
                {
                    // Title exists only on markup annotations (e.g., TextAnnotation, HighlightAnnotation, etc.).
                    if (a is MarkupAnnotation markup && !string.IsNullOrWhiteSpace(markup.Title))
                        return markup.Title;
                    return "Unknown";
                })
                .OrderBy(g => g.Key);

            // Write the report.
            using (StreamWriter writer = new StreamWriter(reportPath, false))
            {
                writer.WriteLine($"Annotation Report for \"{Path.GetFileName(inputPdfPath)}\"");
                writer.WriteLine($"Generated on {DateTime.Now}");
                writer.WriteLine(new string('=', 50));
                writer.WriteLine();

                foreach (var group in grouped)
                {
                    writer.WriteLine($"Author: {group.Key}");
                    writer.WriteLine($"Total Annotations: {group.Count()}");
                    writer.WriteLine("Details:");

                    foreach (Annotation ann in group)
                    {
                        // Basic details: page number, type, and contents (if any).
                        string typeName = ann.AnnotationType.ToString();
                        string contents = string.IsNullOrWhiteSpace(ann.Contents) ? "(no contents)" : ann.Contents.Trim();
                        writer.WriteLine($"  - Page {ann.PageIndex}: [{typeName}] {contents}");
                    }

                    writer.WriteLine(); // blank line between authors
                }
            }

            // No need to call editor.Save() because we only read annotations.
        }

        Console.WriteLine($"Annotation report written to \"{reportPath}\"");
    }

    // Example usage
    static void Main()
    {
        const string inputPdf  = "sample.pdf";
        const string reportTxt = "annotation_report.txt";

        ReportAnnotationsByAuthor(inputPdf, reportTxt);
    }
}
