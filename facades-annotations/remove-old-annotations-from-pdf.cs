using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationCleaner
{
    /// <summary>
    /// Removes annotations older than the specified number of days from the PDF.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the cleaned PDF will be saved.</param>
    /// <param name="daysThreshold">Annotations older than this many days will be removed.</param>
    public static void RemoveOldAnnotations(string inputPdf, string outputPdf, int daysThreshold)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            DateTime cutoffDate = DateTime.Now.AddDays(-daysThreshold);

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annots = page.Annotations;

                // Delete annotations in reverse order to keep indexes valid
                for (int i = annots.Count; i >= 1; i--)
                {
                    Annotation annot = annots[i];

                    // Aspose.Pdf.Annotation provides the Modified property only.
                    // Use it as the annotation's date. If Modified is not set it will be DateTime.MinValue.
                    DateTime annotDate = annot.Modified;

                    if (annotDate < cutoffDate)
                    {
                        annots.Delete(i); // Delete by index (1‑based)
                    }
                }
            }

            // Save the cleaned document (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Old annotations removed. Cleaned file saved to '{outputPdf}'.");
    }

    // Example usage
    static void Main()
    {
        const string inputPath = "batch_input.pdf";
        const string outputPath = "batch_cleaned.pdf";
        const int days = 30; // Remove annotations older than 30 days

        RemoveOldAnnotations(inputPath, outputPath, days);
    }
}
