using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns a list of annotation names whose author (stored in the Subject of a markup annotation) matches the supplied filter.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <param name="authorFilter">Author string to filter annotations (case‑insensitive).</param>
    /// <returns>List of annotation names.</returns>
    public static List<string> GetAnnotationNamesByAuthor(string pdfPath, string authorFilter)
    {
        var matchingNames = new List<string>();

        // Validate input file existence.
        if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
            return matchingNames;

        // Load the PDF document.
        Document pdfDocument;
        try
        {
            pdfDocument = new Document(pdfPath);
        }
        catch (Exception)
        {
            // If the file cannot be opened (e.g., encrypted), return empty list.
            return matchingNames;
        }

        // Iterate through all pages and their annotations.
        foreach (Page page in pdfDocument.Pages)
        {
            if (page.Annotations == null) continue;

            foreach (Annotation annotation in page.Annotations)
            {
                // Only markup annotations expose the Subject property that we use as "author".
                if (annotation is MarkupAnnotation markup &&
                    !string.IsNullOrEmpty(markup.Subject) &&
                    string.Equals(markup.Subject, authorFilter, StringComparison.OrdinalIgnoreCase))
                {
                    // Name is defined on the base Annotation class.
                    if (!string.IsNullOrEmpty(annotation.Name))
                        matchingNames.Add(annotation.Name);
                }
            }
        }

        return matchingNames;
    }
}

// Added entry point to satisfy the executable project requirement.
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (commented out to keep the method side‑effect free):
        // var names = PdfAnnotationHelper.GetAnnotationNamesByAuthor("sample.pdf", "John Doe");
        // Console.WriteLine(string.Join(", ", names));
    }
}