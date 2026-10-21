using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class AnnotationReporter
{
    // Returns a dictionary where the key is the page number (1‑based) and the value is the annotation count on that page.
    public static Dictionary<int, int> GetAnnotationsCountPerPage(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        var counts = new Dictionary<int, int>();

        // Load the PDF once and iterate through its pages.
        // Document implements IDisposable, so we wrap it in a using block for deterministic cleanup.
        using (Document doc = new Document(pdfPath))
        {
            int pageCount = doc.Pages.Count;
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Each Page object exposes an Annotations collection.
                // The collection may be null, but in current Aspose.Pdf versions it is always instantiated.
                int annotationCount = doc.Pages[pageNumber].Annotations?.Count ?? 0;
                counts[pageNumber] = annotationCount;
            }
        }

        return counts;
    }

    // Example usage
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        try
        {
            Dictionary<int, int> pageAnnotationCounts = GetAnnotationsCountPerPage(inputPdf);

            Console.WriteLine($"Annotation counts for '{inputPdf}':");
            foreach (var kvp in pageAnnotationCounts)
            {
                Console.WriteLine($"Page {kvp.Key}: {kvp.Value} annotation(s)");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
