using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns true if the PDF contains any annotations whose Hidden flag is set
    /// after the document has been loaded (sanitization is applied automatically).
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file to inspect.</param>
    /// <returns>True if at least one hidden annotation is present; otherwise, false.</returns>
    public static bool ContainsHiddenAnnotations(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"File not found: {pdfPath}", pdfPath);

        // Load the document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Signature sanitization is enabled by default; ensure it is on.
            doc.EnableSignatureSanitization = true;

            // Iterate through all pages and their annotations.
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    // Check if the Hidden flag is set.
                    if ((annotation.Flags & AnnotationFlags.Hidden) == AnnotationFlags.Hidden)
                        return true;
                }
            }

            // No hidden annotations were found.
            return false;
        }
    }
}

// Minimal entry point required for a console‑application project.
public class Program
{
    public static void Main(string[] args)
    {
        // Optional demo – can be removed or left empty.
        // if (args.Length > 0)
        // {
        //     bool hasHidden = PdfAnnotationHelper.ContainsHiddenAnnotations(args[0]);
        //     Console.WriteLine(hasHidden ? "Hidden annotations found." : "No hidden annotations.");
        // }
    }
}