using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class HiddenAnnotationChecker
{
    /// <summary>
    /// Returns <c>true</c> if any hidden annotations are still present in the PDF after it has been sanitized.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>True when a hidden annotation is found; otherwise false.</returns>
    /// <exception cref="ArgumentException">When <paramref name="pdfPath"/> is null or empty.</exception>
    /// <exception cref="FileNotFoundException">When the file does not exist on disk.</exception>
    public static bool HasHiddenAnnotations(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"The PDF file '{pdfPath}' could not be found.", pdfPath);

        // Load the document and perform a manual sanitization (Aspose.Pdf.Document does not expose a Sanitize() method).
        using (Document doc = new Document(pdfPath))
        {
            ManualSanitize(doc);

            // After sanitization, walk through all pages and their annotations to see if any hidden ones remain.
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    if ((annotation.Flags & AnnotationFlags.Hidden) != 0)
                        return true; // hidden annotation still present
                }
            }
        }

        return false; // no hidden annotations after sanitization
    }

    /// <summary>
    /// Performs a sanitization routine similar to Document.Sanitize():
    ///   • Removes document‑level JavaScript (OpenAction).
    ///   • Clears page‑level JavaScript actions.
    ///   • Deletes embedded file attachment annotations.
    ///   • Removes annotations that are flagged as Hidden.
    /// </summary>
    private static void ManualSanitize(Document doc)
    {
        // Remove document‑level JavaScript.
        doc.OpenAction = null;

        // Iterate through each page.
        foreach (Page page in doc.Pages)
        {
            // Clear page‑level JavaScript actions.
            if (page.Actions != null)
            {
                page.Actions.OnOpen = null;
                page.Actions.OnClose = null;
            }

            // Remove unwanted annotations (file attachments and hidden annotations).
            // Iterate backwards because we are deleting items from the collection.
            for (int i = page.Annotations.Count; i >= 1; i--)
            {
                Annotation ann = page.Annotations[i];

                // Delete file attachment annotations (embedded files).
                if (ann is FileAttachmentAnnotation)
                {
                    page.Annotations.Delete(i);
                    continue;
                }

                // Delete annotations that are marked as Hidden.
                if ((ann.Flags & AnnotationFlags.Hidden) != 0)
                {
                    page.Annotations.Delete(i);
                }
            }
        }
    }

    // Example usage
    public static void Main(string[] args)
    {
        // Allow the caller to pass the PDF path as a command‑line argument; fallback to a default name.
        string inputPdf = args.Length > 0 ? args[0] : "sample.pdf";

        try
        {
            bool hasHidden = HasHiddenAnnotations(inputPdf);
            Console.WriteLine(hasHidden ? "Hidden annotations found after sanitization." : "No hidden annotations after sanitization.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
