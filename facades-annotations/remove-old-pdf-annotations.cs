using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationCleaner
{
    /// <summary>
    /// Removes all annotations older than <paramref name="daysThreshold"/> days from the PDF at <paramref name="inputPath"/>
    /// and saves the cleaned document to <paramref name="outputPath"/>.
    /// </summary>
    public static void CleanOldAnnotations(string inputPath, string outputPath, int daysThreshold)
    {
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input file not found: {inputPath}");

        // Calculate the cutoff date. Annotations with a modification date earlier than this will be removed.
        DateTime cutoffDate = DateTime.Now.AddDays(-daysThreshold);

        // Use PdfAnnotationEditor (a Facades class) to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF.
            editor.BindPdf(inputPath);

            // Access the underlying Document to enumerate pages.
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Collect indexes of annotations that are older than the cutoff.
                List<int> indexesToDelete = new List<int>();

                // AnnotationCollection uses 1‑based indexing.
                for (int i = 1; i <= annotations.Count; i++)
                {
                    Annotation annot = annotations[i];

                    // The Modified property holds the last modification date of the annotation.
                    // It is a non‑nullable DateTime, so we compare it directly.
                    if (annot.Modified < cutoffDate)
                    {
                        indexesToDelete.Add(i);
                    }
                }

                // Delete collected annotations in reverse order to keep indexes valid.
                for (int i = indexesToDelete.Count - 1; i >= 0; i--)
                {
                    annotations.Delete(indexesToDelete[i]);
                }
            }

            // Save the cleaned PDF.
            editor.Save(outputPath);
        }
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "batch_input.pdf";
        const string outputPdf = "batch_cleaned.pdf";
        const int maxAgeDays = 30; // Remove annotations older than 30 days.

        try
        {
            CleanOldAnnotations(inputPdf, outputPdf, maxAgeDays);
            Console.WriteLine($"Old annotations removed. Cleaned file saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during cleanup: {ex.Message}");
        }
    }
}
