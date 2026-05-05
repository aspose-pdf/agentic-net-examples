using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns the names of all annotations whose author (Title) matches the specified author string.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    /// <param name="author">Author name to filter by (matches the Title property of markup annotations).</param>
    /// <returns>List of annotation names.</returns>
    public static List<string> GetAnnotationNamesByAuthor(string pdfPath, string author)
    {
        var matchingNames = new List<string>();

        // Use PdfAnnotationEditor facade to open the document.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Access the underlying Document.
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over the annotations on the current page.
                foreach (Annotation annotation in page.Annotations)
                {
                    // Only markup annotations have the Title property (author).
                    if (annotation is MarkupAnnotation markup && markup.Title == author)
                    {
                        // Annotation.Name may be null; add only if a name is present.
                        if (!string.IsNullOrEmpty(annotation.Name))
                        {
                            matchingNames.Add(annotation.Name);
                        }
                    }
                }
            }

            // No need to save; we are only reading.
        }

        return matchingNames;
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The method can be called from here if needed.
        // Example (commented out):
        // var names = PdfAnnotationHelper.GetAnnotationNamesByAuthor("sample.pdf", "John Doe");
        // Console.WriteLine(string.Join(", ", names));
    }
}