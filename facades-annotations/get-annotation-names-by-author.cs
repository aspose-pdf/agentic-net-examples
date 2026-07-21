using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns a list of annotation names whose author matches the specified author string.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    /// <param name="author">Author name to filter annotations.</param>
    /// <returns>List of annotation names.</returns>
    public static List<string> GetAnnotationNamesByAuthor(string pdfPath, string author)
    {
        // Ensure the file exists before processing.
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        var result = new List<string>();

        // PdfAnnotationEditor implements IDisposable, so use a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor.
            editor.BindPdf(pdfPath);

            // Access the underlying Document object.
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing).
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate through all annotations on the page.
                foreach (Annotation annotation in page.Annotations)
                {
                    // Only markup annotations have a Subject property that can be used as "author".
                    if (annotation is MarkupAnnotation markup &&
                        !string.IsNullOrEmpty(markup.Subject) &&
                        markup.Subject.Equals(author, StringComparison.OrdinalIgnoreCase))
                    {
                        // Annotation.Name may be null; handle gracefully.
                        if (!string.IsNullOrEmpty(annotation.Name))
                            result.Add(annotation.Name);
                    }
                }
            }
        }

        return result;
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library functionality is exposed via PdfAnnotationHelper.
    }
}