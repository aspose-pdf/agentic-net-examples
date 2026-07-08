using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns the names of all annotations whose author (stored in the Subject property of a markup annotation) matches the specified value.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    /// <param name="author">Author string to filter by.</param>
    /// <returns>List of annotation names.</returns>
    public static List<string> GetAnnotationNamesByAuthor(string pdfPath, string author)
    {
        // Validate inputs
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (author == null)
            throw new ArgumentNullException(nameof(author));

        var matchingNames = new List<string>();

        // Use PdfAnnotationEditor (facade) to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(pdfPath);

            // Determine the page range (1‑based indexing)
            int startPage = 1;
            int endPage = editor.Document.Pages.Count;

            // Retrieve all annotation types defined in the enum
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Extract all annotations in the specified page range
            IList<Annotation> annotations = editor.ExtractAnnotations(startPage, endPage, allTypes);

            // Filter by author (stored in Subject of markup annotations) and collect the annotation names
            foreach (var annotation in annotations)
            {
                if (annotation is MarkupAnnotation markup &&
                    !string.IsNullOrEmpty(markup.Subject) &&
                    string.Equals(markup.Subject, author, StringComparison.Ordinal))
                {
                    // Annotation.Name holds the identifier; add it to the result list
                    matchingNames.Add(annotation.Name);
                }
            }
        }

        return matchingNames;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library method can be called from other code.
    }
}