using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Security.HiddenDataSanitization;

public static class PdfSanitizer
{
    /// <summary>
    /// Returns true if any hidden annotations are still present after sanitizing the PDF.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    public static bool HasHiddenAnnotationsAfterSanitization(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"File not found: {pdfPath}");

        // Wrap Document in a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = new Document(pdfPath))
        {
            // Use HiddenDataSanitizer when Document.Sanitize() is unavailable (cs1061 fix)
            var options = new HiddenDataSanitizationOptions
            {
                RemoveAnnotations = true // ensures annotations are stripped during sanitization
            };
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Examine each annotation on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Annotation.Flags is a bitmask; check for the Hidden flag
                    if ((annotation.Flags & AnnotationFlags.Hidden) == AnnotationFlags.Hidden)
                    {
                        // Hidden annotation found after sanitization
                        return true;
                    }
                }
            }

            // No hidden annotations remain
            return false;
        }
    }
}

// Dummy entry point to satisfy the compiler when building an executable project.
public static class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is exercised via PdfSanitizer.
    }
}