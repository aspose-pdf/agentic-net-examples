using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationReporter
{
    // Extracts annotations, groups them by author (Title property of markup annotations), and prints a simple report.
    public static void ReportAnnotationsByAuthor(string pdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfAnnotationEditor is a Facades class; it implements IDisposable via SaveableFacade.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(pdfPath);

            // Retrieve total page count from the underlying Document.
            int pageCount = editor.Document.Pages.Count;

            // Get all possible annotation types.
            AnnotationType[] allTypes = Enum.GetValues(typeof(AnnotationType))
                                            .Cast<AnnotationType>()
                                            .ToArray();

            // Extract every annotation from the whole document.
            IList<Annotation> allAnnotations = editor.ExtractAnnotations(1, pageCount, allTypes);

            // Group annotations by the author name stored in the Title property of markup annotations.
            var groups = allAnnotations
                         .GroupBy(a =>
                         {
                             // Title exists only on markup annotations.
                             if (a is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                                 return markup.Title;
                             return "Unknown Author";
                         })
                         .OrderBy(g => g.Key);

            // Output the report.
            Console.WriteLine($"Annotation report for '{Path.GetFileName(pdfPath)}':");
            foreach (var group in groups)
            {
                Console.WriteLine($"\nAuthor: {group.Key}");
                Console.WriteLine($"Total Annotations: {group.Count()}");
                foreach (Annotation ann in group)
                {
                    // Basic details: type, page index, and contents (if any).
                    Console.WriteLine($"  - Type: {ann.AnnotationType}, Page: {ann.PageIndex}, Contents: {ann.Contents}");
                }
            }

            // No modifications were made, but invoking Save satisfies the lifecycle rule.
            // The file is saved back to the same location (overwrites original).
            editor.Save(pdfPath);
        }
    }

    // Example entry point.
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        ReportAnnotationsByAuthor(inputPdf);
    }
}
