using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationCleanup
{
    /// <summary>
    /// Removes annotations older than the specified number of days from a PDF file.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the cleaned PDF will be saved.</param>
    /// <param name="daysThreshold">Annotations older than this many days will be deleted.</param>
    public static void RemoveOldAnnotations(string inputPdf, string outputPdf, int daysThreshold)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Calculate the cutoff date.
        DateTime cutoff = DateTime.Now.AddDays(-daysThreshold);

        // Use PdfAnnotationEditor facade to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF file.
            editor.BindPdf(inputPdf);

            // Access the underlying Document.
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annColl = page.Annotations;

                // Iterate backwards so that deletions do not affect the loop index.
                for (int i = annColl.Count; i >= 1; i--)
                {
                    Annotation ann = annColl[i];

                    // Only markup annotations expose date information.
                    if (ann is MarkupAnnotation markup)
                    {
                        // Prefer Modified; fall back to CreationDate if Modified is not set.
                        DateTime annDate = markup.Modified != DateTime.MinValue
                                            ? markup.Modified
                                            : markup.CreationDate;

                        // Delete annotation if it is older than the cutoff.
                        if (annDate < cutoff)
                        {
                            annColl.Delete(i);
                        }
                    }
                    // Non‑markup annotations (e.g., link, widget) do not have date properties – skip them.
                }
            }

            // Save the cleaned PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Old annotations removed. Output saved to '{outputPdf}'.");
    }

    // Example usage.
    static void Main()
    {
        const string inputPath  = "batch_input.pdf";
        const string outputPath = "batch_cleaned.pdf";
        const int  daysOld     = 30; // Remove annotations older than 30 days.

        RemoveOldAnnotations(inputPath, outputPath, daysOld);
    }
}
