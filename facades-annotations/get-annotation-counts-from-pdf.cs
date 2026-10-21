using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    // Returns a dictionary mapping annotation type names to their occurrence counts.
    public static Dictionary<string, int> GetAnnotationCounts(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Load the PDF document.
        Document pdfDocument = new Document(pdfPath);

        // Prepare the result dictionary.
        Dictionary<string, int> typeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Iterate through all pages and their annotations.
        foreach (Page page in pdfDocument.Pages)
        {
            foreach (Annotation annotation in page.Annotations)
            {
                // Use the enum name if available; otherwise fall back to the class name.
                string typeName = annotation.AnnotationType != AnnotationType.Unknown
                    ? annotation.AnnotationType.ToString()
                    : annotation.GetType().Name;

                if (typeCounts.ContainsKey(typeName))
                    typeCounts[typeName]++;
                else
                    typeCounts[typeName] = 1;
            }
        }

        return typeCounts;
    }

    // Simple entry point to make the project compile as an executable.
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: PdfAnnotationHelper <pdf-path>");
            return;
        }

        try
        {
            var counts = GetAnnotationCounts(args[0]);
            foreach (var kvp in counts)
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