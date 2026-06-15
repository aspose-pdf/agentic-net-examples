using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace AsposePdfApi
{
    public static class PdfAnnotationHelper
    {
        /// <summary>
        /// Returns a dictionary where the key is the annotation type name
        /// and the value is the number of occurrences of that type in the PDF.
        /// </summary>
        /// <param name="pdfPath">Path to the PDF file.</param>
        /// <returns>Dictionary of annotation type names to counts.</returns>
        public static Dictionary<string, int> GetAnnotationCounts(string pdfPath)
        {
            // Validate input
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            // Use PdfAnnotationEditor (facade) to work with annotations.
            // The facade implements IDisposable, so wrap it in a using block.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF file to the editor.
                editor.BindPdf(pdfPath);

                // Determine the total number of pages in the document.
                int pageCount = editor.Document.Pages.Count;

                // Extract all annotations from the whole document.
                // Passing an empty string array retrieves annotations of all types.
                IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, new string[0]);

                // Prepare the result dictionary.
                Dictionary<string, int> counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through the extracted annotations and count each type.
                foreach (Annotation annotation in annotations)
                {
                    // AnnotationType is an enum; use its name as the dictionary key.
                    string typeName = annotation.AnnotationType.ToString();

                    if (counts.ContainsKey(typeName))
                        counts[typeName] += 1;
                    else
                        counts[typeName] = 1;
                }

                return counts;
            }
        }
    }

    // Minimal entry point required for a console application.
    // It does not perform any work; it simply satisfies the compiler.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage (optional). Commented out to avoid runtime errors when no PDF is supplied.
            // if (args.Length > 0)
            // {
            //     var result = PdfAnnotationHelper.GetAnnotationCounts(args[0]);
            //     foreach (var kvp in result)
            //         Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            // }
        }
    }
}
