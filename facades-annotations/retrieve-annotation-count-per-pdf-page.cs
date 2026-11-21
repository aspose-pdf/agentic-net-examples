using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class AnnotationReporter
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Retrieve annotation counts per page
        Dictionary<int, int> pageAnnotationCounts = GetAnnotationsCountPerPage(pdfPath);

        // Report the results
        foreach (KeyValuePair<int, int> kvp in pageAnnotationCounts)
        {
            Console.WriteLine($"Page {kvp.Key}: {kvp.Value} annotation(s)");
        }
    }

    // Returns a dictionary where key = page number (1‑based) and value = annotation count
    static Dictionary<int, int> GetAnnotationsCountPerPage(string pdfFilePath)
    {
        var counts = new Dictionary<int, int>();

        // Load the document; Aspose.Pdf uses 1‑based page indexing
        Document pdfDoc = new Document(pdfFilePath);
        int pageCount = pdfDoc.Pages.Count;

        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
        {
            // Directly use the Annotations collection of the page
            int annotationCount = pdfDoc.Pages[pageNumber].Annotations?.Count ?? 0;
            counts[pageNumber] = annotationCount;
        }

        return counts;
    }
}
