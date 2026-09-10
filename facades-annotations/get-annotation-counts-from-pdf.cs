using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace AsposePdfApi
{
    public static class PdfAnnotationHelper
    {
        /// <summary>
        /// Returns a dictionary where the key is the annotation type name
        /// (e.g., "Text", "Highlight") and the value is the number of
        /// occurrences of that type in the specified PDF file.
        /// </summary>
        /// <param name="pdfPath">Full path to the PDF document.</param>
        /// <returns>Dictionary mapping annotation type names to their counts.</returns>
        public static Dictionary<string, int> GetAnnotationCounts(string pdfPath)
        {
            // Guard against missing file.
            if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
                throw new ArgumentException("PDF file not found.", nameof(pdfPath));

            var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Use the core Document class only to obtain the total page count
            // (required for the ExtractAnnotations call). Follow the
            // document‑disposal‑with‑using rule.
            using (Document doc = new Document(pdfPath))
            {
                int pageCount = doc.Pages.Count;

                // PdfAnnotationEditor implements IDisposable, so wrap it in using.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);

                    // Extract all annotations. Passing null for the type filter
                    // is ambiguous because there are overloads that accept string[]
                    // and AnnotationType[]. Cast to the desired overload to remove
                    // the ambiguity.
                    IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, (AnnotationType[])null);

                    foreach (var annotation in annotations)
                    {
                        string typeName = annotation.AnnotationType.ToString();
                        if (counts.ContainsKey(typeName))
                            counts[typeName]++;
                        else
                            counts[typeName] = 1;
                    }

                    // No modifications are made, but the save rule requires a save call.
                    // Saving back to the same file preserves the original content.
                    editor.Save(pdfPath);
                }
            }

            return counts;
        }
    }

    class Program
    {
        // Entry point required for a console‑application project.
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
                var counts = PdfAnnotationHelper.GetAnnotationCounts(pdfPath);
                Console.WriteLine("Annotation counts:");
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
