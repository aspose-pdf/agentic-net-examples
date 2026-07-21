using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Load the document to obtain the total page count.
        using (Document doc = new Document(pdfPath))
        {
            int pageCount = doc.Pages.Count;

            // Use PdfAnnotationEditor (Facade API) to extract all annotations.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfPath);

                // Passing an empty string array extracts annotations of all types.
                string[] allTypes = new string[0];
                IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, allTypes);

                var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                foreach (Annotation ann in annotations)
                {
                    // AnnotationType is an enum; use its name as the dictionary key.
                    string typeName = ann.AnnotationType.ToString();

                    if (result.ContainsKey(typeName))
                        result[typeName]++;
                    else
                        result[typeName] = 1;
                }

                return result;
            }
        }
    }
}

// Added entry point to satisfy the console‑application build requirement.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library class can be used from other code.
    }
}
