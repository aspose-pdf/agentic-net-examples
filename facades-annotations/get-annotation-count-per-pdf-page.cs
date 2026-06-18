using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Annotations;        // Annotation types

class AnnotationReport
{
    /// <summary>
    /// Retrieves the number of annotations on each page of the specified PDF.
    /// </summary>
    /// <param name="pdfPath">Path to the input PDF file.</param>
    /// <returns>
    /// A dictionary where the key is the 1‑based page number and the value is the count of annotations on that page.
    /// </returns>
    public static Dictionary<int, int> GetAnnotationsCountPerPage(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Always wrap Document in a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            var result = new Dictionary<int, int>();

            // Pages collection is 1‑based.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // AnnotationCollection.Count gives the number of annotations on the page.
                int count = page.Annotations.Count;
                result[i] = count;
            }

            return result;
        }
    }

    // Example usage
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        try
        {
            Dictionary<int, int> counts = GetAnnotationsCountPerPage(inputPdf);

            Console.WriteLine($"Annotation counts for '{inputPdf}':");
            foreach (var kvp in counts)
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