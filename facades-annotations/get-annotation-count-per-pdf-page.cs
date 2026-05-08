using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfAnnotationReporting
{
    /// <summary>
    /// Provides functionality to report the number of annotations on each page of a PDF document.
    /// </summary>
    public static class AnnotationReporter
    {
        /// <summary>
        /// Retrieves a dictionary where the key is the 1‑based page number and the value is the count of annotations on that page.
        /// </summary>
        /// <param name="pdfPath">Full path to the PDF file.</param>
        /// <returns>Dictionary mapping page numbers to annotation counts.</returns>
        public static Dictionary<int, int> GetAnnotationsCountPerPage(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be a non‑empty string.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // Use PdfAnnotationEditor (Facade) to bind the PDF.
            // The editor implements IDisposable, so wrap it in a using block.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfPath);               // Load the document.
                Document doc = editor.Document;        // Access the underlying Document.

                var pageAnnotationCounts = new Dictionary<int, int>();

                // Pages collection is 1‑based.
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    int annotationCount = page.Annotations.Count; // AnnotationCollection.Count
                    pageAnnotationCounts[pageIndex] = annotationCount;
                }

                return pageAnnotationCounts;
            }
        }

        // Example usage.
        public static void Main()
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
}