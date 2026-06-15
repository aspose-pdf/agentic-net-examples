using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns a list of annotation titles (or names) whose author (stored in Subject) matches the specified value.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    /// <param name="author">Author string to filter annotations.</param>
    /// <returns>List of annotation titles belonging to the given author.</returns>
    public static List<string> GetAnnotationNamesByAuthor(string pdfPath, string author)
    {
        // Validate input
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        var result = new List<string>();

        // Load the PDF document directly – no need for PdfAnnotationEditor when only reading.
        Document doc = new Document(pdfPath);

        // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
        for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
        {
            Page page = doc.Pages[pageIndex];

            // Iterate over each annotation on the current page.
            foreach (Annotation annotation in page.Annotations)
            {
                // Only markup annotations expose Subject (author) and Title.
                if (annotation is MarkupAnnotation markup)
                {
                    // Compare the Subject property with the requested author (case‑sensitive).
                    if (string.Equals(markup.Subject, author, StringComparison.Ordinal))
                    {
                        // Title may be null – replace with empty string if so.
                        result.Add(markup.Title ?? string.Empty);
                    }
                }
            }
        }

        return result;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main()
    {
        // This method is intentionally left empty.
        // You can place test code here if you wish to run the helper manually.
    }
}