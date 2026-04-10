using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        ReportAnnotationsByAuthor(inputPdf);
    }

    // Extracts all annotations, groups them by the author (Title property of markup annotations) and prints a simple report.
    static void ReportAnnotationsByAuthor(string pdfPath)
    {
        // Open the PDF document (ensures proper disposal).
        using (Document doc = new Document(pdfPath))
        {
            // Initialize the annotation editor on the opened document.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                // Retrieve all annotation types defined in the enum.
                AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

                // Extract annotations from the first to the last page.
                IList<Annotation> annotations = editor.ExtractAnnotations(1, doc.Pages.Count, allTypes);

                // Group annotations by their author (Title property of MarkupAnnotation). If Title is null/empty, use "<No Author>".
                var groups = new Dictionary<string, List<Annotation>>(StringComparer.OrdinalIgnoreCase);
                foreach (Annotation annot in annotations)
                {
                    string author;
                    if (annot is MarkupAnnotation markup && !string.IsNullOrWhiteSpace(markup.Title))
                    {
                        author = markup.Title;
                    }
                    else
                    {
                        author = "<No Author>";
                    }

                    if (!groups.TryGetValue(author, out List<Annotation> list))
                    {
                        list = new List<Annotation>();
                        groups[author] = list;
                    }
                    list.Add(annot);
                }

                // Output the grouped report.
                Console.WriteLine($"Annotation report for '{Path.GetFileName(pdfPath)}':");
                foreach (var kvp in groups)
                {
                    Console.WriteLine($"Author: {kvp.Key}");
                    Console.WriteLine($"  Count: {kvp.Value.Count}");
                    Console.WriteLine("  Types:");
                    foreach (Annotation a in kvp.Value)
                    {
                        Console.WriteLine($"    - {a.AnnotationType}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
