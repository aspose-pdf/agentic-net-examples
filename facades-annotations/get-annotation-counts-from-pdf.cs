using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfAnnotationHelper
{
    // Returns a dictionary where the key is the annotation type name
    // (e.g., "TextAnnotation", "LinkAnnotation") and the value is the
    // number of occurrences of that type in the specified PDF.
    public static Dictionary<string, int> GetAnnotationCounts(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Use a using block for deterministic disposal of the Document.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Aspose.Pdf.Page page = doc.Pages[i];

                // Iterate over all annotations on the current page.
                foreach (Aspose.Pdf.Annotations.Annotation annot in page.Annotations)
                {
                    string typeName = annot.GetType().Name;

                    if (counts.ContainsKey(typeName))
                        counts[typeName]++;
                    else
                        counts[typeName] = 1;
                }
            }
        }

        return counts;
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        try
        {
            Dictionary<string, int> annotationCounts = GetAnnotationCounts(inputPdf);

            Console.WriteLine($"Annotation counts for '{inputPdf}':");
            foreach (var kvp in annotationCounts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}