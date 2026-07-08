using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationCleanup
{
    /// <summary>
    /// Removes all annotations older than the specified number of days from the input PDF
    /// and saves the cleaned document to the output path.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF file.</param>
    /// <param name="outputPdf">Path where the cleaned PDF will be saved.</param>
    /// <param name="daysThreshold">Annotations older than this many days will be removed.</param>
    public static void RemoveOldAnnotations(string inputPdf, string outputPdf, int daysThreshold)
    {
        if (!File.Exists(inputPdf))
            throw new FileNotFoundException($"Input file not found: {inputPdf}");

        // Calculate the cutoff date.
        DateTime cutoffDate = DateTime.Now.AddDays(-daysThreshold);

        // Initialize the annotation editor and bind the PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // Access the underlying Document to iterate pages and annotations.
        Document doc = editor.Document;

        // Pages are 1‑based in Aspose.Pdf.
        for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
        {
            Page page = doc.Pages[pageIndex];
            AnnotationCollection annotations = page.Annotations;

            // Iterate backwards so that deletions do not affect the loop index.
            for (int annIndex = annotations.Count; annIndex >= 1; annIndex--)
            {
                Annotation annotation = annotations[annIndex];

                // The Modified property holds the last modification date of the annotation.
                // If the property is not set (MinValue), we keep the annotation.
                if (annotation.Modified != DateTime.MinValue && annotation.Modified < cutoffDate)
                {
                    // Delete the annotation by its index.
                    annotations.Delete(annIndex);
                }
            }
        }

        // Save the cleaned PDF.
        editor.Save(outputPdf);
    }

    // Example usage.
    static void Main()
    {
        const string inputPath = "batch_input.pdf";
        const string outputPath = "batch_cleaned.pdf";
        const int days = 30; // Remove annotations older than 30 days.

        try
        {
            RemoveOldAnnotations(inputPath, outputPath, days);
            Console.WriteLine($"Old annotations removed. Cleaned file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during cleanup: {ex.Message}");
        }
    }
}