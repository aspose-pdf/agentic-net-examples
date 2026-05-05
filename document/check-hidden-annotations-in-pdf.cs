using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns true if the PDF contains any annotations whose Hidden flag is set after the document has been sanitized.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file to inspect.</param>
    /// <returns>True when at least one hidden annotation is present; otherwise false.</returns>
    public static bool ContainsHiddenAnnotations(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Load the document using the core Document API (no Facades).
        using (Document doc = new Document(pdfPath))
        {
            // NOTE: In recent Aspose.Pdf versions the Document class does not expose a public
            // Sanitize() method. The original intent was to remove hidden content before the
            // check. Because the API does not provide this operation, we simply inspect the
            // annotations as‑is. If a future version re‑introduces Document.Sanitize(), the call
            // can be added back without changing the surrounding logic.

            // Iterate through all pages and their annotations.
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    // The Hidden flag is part of the AnnotationFlags enum.
                    if ((annotation.Flags & AnnotationFlags.Hidden) == AnnotationFlags.Hidden)
                    {
                        // At least one hidden annotation found after (or without) sanitization.
                        return true;
                    }
                }
            }

            // No hidden annotations were found.
            return false;
        }
    }

    // ---------------------------------------------------------------------
    // A minimal entry point is added so the project compiles as a console
    // application. The method is not required for the library functionality
    // but satisfies the compiler's expectation of a static Main method.
    // ---------------------------------------------------------------------
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: PdfAnnotationHelper <pdfPath>");
            return;
        }

        string pdfPath = args[0];
        try
        {
            bool hasHidden = ContainsHiddenAnnotations(pdfPath);
            Console.WriteLine(hasHidden ? "Hidden annotations were found." : "No hidden annotations were found.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}