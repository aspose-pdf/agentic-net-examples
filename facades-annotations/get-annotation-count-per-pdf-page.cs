using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfAnnotationReporter
    {
        /// <summary>
        /// Retrieves the total number of annotations on each page of the specified PDF.
        /// Returns a dictionary where the key is the 1‑based page number and the value is the annotation count.
        /// </summary>
        /// <param name="pdfPath">Path to the PDF file.</param>
        /// <returns>Dictionary of page numbers to annotation counts.</returns>
        public static Dictionary<int, int> GetAnnotationsCountPerPage(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            // Use PdfAnnotationEditor (a Facades class) to bind the PDF.
            // The editor implements IDisposable, so wrap it in a using block.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF file to the editor.
                editor.BindPdf(pdfPath);

                // Access the underlying Document via the Document property.
                Document doc = editor.Document;

                // Prepare the result container.
                var counts = new Dictionary<int, int>();

                // Pages collection is 1‑based.
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Each Page has an Annotations collection with a Count property.
                    int annotationCount = doc.Pages[i].Annotations.Count;
                    counts[i] = annotationCount;
                }

                // No need to call Save or Close on the editor beyond the using block.
                return counts;
            }
        }
    }

    // Simple console entry point so the project compiles as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // If a PDF path is supplied as the first argument, display the annotation counts.
            if (args.Length > 0)
            {
                string pdfPath = args[0];
                try
                {
                    var result = PdfAnnotationReporter.GetAnnotationsCountPerPage(pdfPath);
                    foreach (var kvp in result)
                    {
                        Console.WriteLine($"Page {kvp.Key}: {kvp.Value} annotation(s)");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: AsposePdfApi <pdf-file-path>");
            }
        }
    }
}
