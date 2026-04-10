using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Returns a dictionary that maps each annotation type present in the PDF to its occurrence count.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>Dictionary where key = AnnotationType, value = count of that type.</returns>
    public static Dictionary<AnnotationType, int> GetAnnotationCounts(string pdfPath)
    {
        // Validate input file.
        if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Use PdfAnnotationEditor (Facade) to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor.
            editor.BindPdf(pdfPath);

            // Determine the total number of pages (1‑based indexing).
            int totalPages = editor.Document.Pages.Count;

            // Retrieve all possible annotation types.
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Extract all annotations from the whole document.
            IList<Annotation> annotations = editor.ExtractAnnotations(1, totalPages, allTypes);

            // Count occurrences per type.
            var counts = new Dictionary<AnnotationType, int>();
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

// ---------------------------------------------------------------------------
// Simple console entry point – required for a project that expects a Main method.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: PdfAnnotationHelper <pdf-file-path>");
            return;
        }

        string pdfPath = args[0];
        try
        {
            var counts = PdfAnnotationHelper.GetAnnotationCounts(pdfPath);
            Console.WriteLine($"Annotation counts for '{Path.GetFileName(pdfPath)}':");
            foreach (var kvp in counts)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}