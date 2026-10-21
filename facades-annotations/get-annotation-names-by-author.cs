using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns the names of all annotations in the specified PDF whose Author matches the given string.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <param name="author">Author name to filter annotations by (case‑sensitive).</param>
    /// <returns>List of annotation names.</returns>
    public static List<string> GetAnnotationNamesByAuthor(string pdfPath, string author)
    {
        // Validate input
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);
        if (author == null)
            throw new ArgumentNullException(nameof(author));

        // Load the PDF document using Aspose.Pdf.Document
        Document doc = new Document(pdfPath);
        List<string> matchingNames = new List<string>();

        // Iterate through each page and collect matching annotations
        foreach (Page page in doc.Pages)
        {
            // Annotations collection may contain different types; we are interested in markup annotations
            foreach (Annotation ann in page.Annotations)
            {
                // Only markup annotations have a Title property that stores the author information
                if (ann is MarkupAnnotation markup && markup.Title != null &&
                    markup.Title.Equals(author, StringComparison.Ordinal))
                {
                    // The Name property holds the annotation's identifier (may be null/empty)
                    if (!string.IsNullOrEmpty(ann.Name))
                        matchingNames.Add(ann.Name);
                }
            }
        }

        return matchingNames;
    }

    // ---------------------------------------------------------------------
    // A minimal entry point is required for the project to compile as an
    // executable. It does not perform any work; it simply satisfies the
    // compiler's expectation of a static Main method.
    // ---------------------------------------------------------------------
    public static void Main(string[] args)
    {
        // No‑op – the library method can be called from other code or unit tests.
    }
}