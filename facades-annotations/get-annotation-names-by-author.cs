using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns the names of all annotations whose Author matches the specified value.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <param name="author">Author string to filter annotations.</param>
    /// <returns>List of annotation names.</returns>
    public static List<string> GetAnnotationNamesByAuthor(string pdfPath, string author)
    {
        var result = new List<string>();

        // Load the PDF directly via the Document class (no Facade API). This avoids
        // locking the Aspose.Pdf assembly during build and is the recommended way
        // for read‑only operations.
        var doc = new Document(pdfPath);

        // Aspose.Pdf pages are 1‑based.
        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
        {
            Page page = doc.Pages[pageNum];

            foreach (Annotation annotation in page.Annotations)
            {
                // The author of a markup annotation is stored in the Title property.
                if (annotation is MarkupAnnotation markup &&
                    !string.IsNullOrEmpty(markup.Title) &&
                    string.Equals(markup.Title, author, StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrEmpty(annotation.Name))
                {
                    result.Add(annotation.Name);
                }
            }
        }

        return result;
    }
}

// Dummy entry point to satisfy the compiler when the project expects a Main method.
public class Program
{
    public static void Main()
    {
        // Example usage (can be removed or replaced by unit tests)
        // var names = PdfAnnotationHelper.GetAnnotationNamesByAuthor("sample.pdf", "John Doe");
        // Console.WriteLine(string.Join(", ", names));
    }
}