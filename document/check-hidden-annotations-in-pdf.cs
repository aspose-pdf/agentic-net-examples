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
    /// <param name="pdfPath">Path to the input PDF file.</param>
    /// <returns>True if hidden annotations remain; otherwise false.</returns>
    public static bool HasHiddenAnnotationsAfterSanitization(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"File not found: {pdfPath}");

        // Load the document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // ---------------------------------------------------------------------
            // Sanitization step.
            // ---------------------------------------------------------------------
            // The Document.Sanitize() method is not available in older Aspose.Pdf
            // versions, so we fall back to the explicit removal of all annotations.
            // HiddenDataSanitizer can also be used when the API is present.
            // ---------------------------------------------------------------------
            try
            {
                // Attempt to use the hidden‑data sanitizer (available in recent versions).
                var options = new HiddenDataSanitizationOptions { RemoveAnnotations = true };
                var sanitizer = new HiddenDataSanitizer(options);
                sanitizer.Sanitize(doc);
            }
            catch (MissingMethodException)
            {
                // If the sanitizer class itself is missing, clear the annotations manually.
                foreach (Page page in doc.Pages)
                {
                    page.Annotations.Clear();
                }
            }
            catch (Exception)
            {
                // Any other unexpected error – fall back to the manual clear as a safety net.
                foreach (Page page in doc.Pages)
                {
                    page.Annotations.Clear();
                }
            }

            // After sanitization, inspect remaining annotations for the Hidden flag.
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot.Flags.HasFlag(AnnotationFlags.Hidden))
                        return true; // Hidden annotation still present.
                }
            }

            // No hidden annotations were found.
            return false;
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is exposed via PdfSanitizer.
    }
}