using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns a list of annotation names whose author (stored in the Subject property of markup annotations) matches the specified value.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    /// <param name="author">Author string to filter annotations.</param>
    /// <returns>List of annotation names.</returns>
    public static List<string> GetAnnotationNamesByAuthor(string pdfPath, string author)
    {
        // Validate input
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Initialize the facade and bind the PDF document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Determine the total number of pages in the document
            int pageCount = editor.Document.Pages.Count;

            // Extract all annotations from the whole document.
            // Cast null to AnnotationType[] to avoid overload ambiguity.
            IList<Annotation> allAnnotations = editor.ExtractAnnotations(1, pageCount, (AnnotationType[])null);

            List<string> matchingNames = new List<string>();

            // Filter annotations by the author (Subject) and collect their names.
            foreach (Annotation annot in allAnnotations)
            {
                // Only markup annotations expose the Subject property that is used for the author.
                if (annot is MarkupAnnotation markup &&
                    !string.IsNullOrEmpty(markup.Subject) &&
                    markup.Subject.Equals(author, StringComparison.OrdinalIgnoreCase))
                {
                    // Annotation.Name can be null; handle gracefully.
                    if (!string.IsNullOrEmpty(markup.Name))
                        matchingNames.Add(markup.Name);
                }
            }

            return matchingNames;
        }
    }

    // A minimal entry point to satisfy the compiler when the project is built as an executable.
    // In a library project this method can be removed.
    public static void Main(string[] args)
    {
        // Example usage (can be removed or replaced by unit tests).
        if (args.Length == 2)
        {
            var result = GetAnnotationNamesByAuthor(args[0], args[1]);
            Console.WriteLine(string.Join(", ", result));
        }
    }
}