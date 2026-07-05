using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class AnnotationCounter
    {
        // Returns a dictionary where the key is the annotation type name
        // and the value is the number of occurrences of that type in the PDF.
        public static Dictionary<string, int> GetAnnotationCounts(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Use PdfAnnotationEditor (Facade) to bind the PDF.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfPath);

                // Access the underlying Document from the editor.
                Document doc = editor.Document;

                // Iterate through all pages (1‑based indexing).
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate through each annotation on the page.
                    foreach (Annotation annotation in page.Annotations)
                    {
                        string typeName = annotation.AnnotationType.ToString();

                        if (counts.ContainsKey(typeName))
                            counts[typeName]++;
                        else
                            counts[typeName] = 1;
                    }
                }
            }

            return counts;
        }
    }

    // Added entry point to satisfy the project’s requirement for a Main method.
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: AsposePdfApi <pdfPath>");
                return;
            }

            string pdfPath = args[0];
            try
            {
                var counts = AnnotationCounter.GetAnnotationCounts(pdfPath);
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
}