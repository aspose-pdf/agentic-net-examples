using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns a dictionary that maps each annotation type present in the PDF to its occurrence count.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>Dictionary where key is AnnotationType and value is the number of occurrences.</returns>
    public static Dictionary<AnnotationType, int> GetAnnotationCounts(string pdfPath)
    {
        // Ensure the PDF file exists before processing.
        if (string.IsNullOrEmpty(pdfPath) || !System.IO.File.Exists(pdfPath))
            throw new ArgumentException("PDF file not found.", nameof(pdfPath));

        // Load the PDF document inside a using block for deterministic disposal.
        // The null‑forgiving operator (!) tells the compiler that pdfPath is not null here
        // (we have already validated it above).
        using (Document doc = new Document(pdfPath!))
        {
            // Initialize the PdfAnnotationEditor facade and bind it to the loaded document.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Retrieve all annotation types defined in the enum.
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Extract all annotations from the first to the last page.
            IList<Annotation> annotations = editor.ExtractAnnotations(1, doc.Pages.Count, allTypes);

            // Count occurrences per annotation type.
            Dictionary<AnnotationType, int> counts = new Dictionary<AnnotationType, int>();
            foreach (Annotation ann in annotations)
            {
                AnnotationType type = ann.AnnotationType;
                if (counts.ContainsKey(type))
                    counts[type]++;
                else
                    counts[type] = 1;
            }

            return counts;
        }
    }
}

// Added entry point to satisfy the executable project requirement.
public class Program
{
    public static void Main(string[] args)
    {
        // Optional demonstration (can be removed or left empty).
        // if (args.Length > 0)
        // {
        //     var result = PdfAnnotationHelper.GetAnnotationCounts(args[0]);
        //     foreach (var kvp in result)
        //         Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        // }
    }
}