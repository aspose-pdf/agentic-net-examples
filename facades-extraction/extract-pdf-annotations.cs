using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // PdfAnnotationEditor
using Aspose.Pdf.Annotations;      // Annotation types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotations.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create the annotation editor and bind the PDF document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);

                // Extract all annotations – the method expects positional arguments
                // (startPage, endPage, AnnotationType[]). Passing an empty array means "no filter" (all types).
                IList<Annotation> allAnnotations = editor.ExtractAnnotations(
                    1,
                    editor.Document.Pages.Count,
                    Array.Empty<AnnotationType>());

                // Write annotation details to a text file. Since Annotation does not expose a PageNumber,
                // we obtain the page index by searching the document pages for the same instance.
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    foreach (Annotation ann in allAnnotations)
                    {
                        // Find the page that contains this annotation (1‑based index)
                        int pageNumber = FindPageNumber(editor.Document, ann);

                        writer.WriteLine($"Page: {pageNumber}");
                        writer.WriteLine($"Subtype: {ann.AnnotationType}");
                        writer.WriteLine($"Contents: {ann.Contents}");

                        // Title exists only on markup annotations (e.g., TextAnnotation)
                        if (ann is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                        {
                            writer.WriteLine($"Title: {markup.Title}");
                        }
                        else
                        {
                            writer.WriteLine("Title: N/A");
                        }

                        writer.WriteLine("---");
                    }
                }

                Console.WriteLine($"Annotations extracted to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper: locate the page index (1‑based) that contains the given annotation instance.
    private static int FindPageNumber(Document doc, Annotation target)
    {
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];
            foreach (Annotation ann in page.Annotations)
            {
                if (ReferenceEquals(ann, target))
                    return i;
            }
        }
        return -1; // not found (should not happen)
    }
}
