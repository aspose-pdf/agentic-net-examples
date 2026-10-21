using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_cleaned.pdf";
        const string logFile = "annotation_deletions.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Prepare the log file (CSV header)
        using (var logWriter = new StreamWriter(logFile, false))
        {
            logWriter.WriteLine("PageNumber,AnnotationName,AnnotationType");
        }

        // Load the PDF document (Aspose.Pdf.Document provides full annotation API)
        Document pdfDoc = new Document(inputPdf);
        int pageCount = pdfDoc.Pages.Count; // 1‑based page indexing

        // Iterate through all pages
        for (int pageNum = 1; pageNum <= pageCount; pageNum++)
        {
            Page page = pdfDoc.Pages[pageNum];
            if (page == null || page.Annotations == null || page.Annotations.Count == 0)
                continue;

            // Collect annotations to delete after logging (cannot modify collection while iterating)
            var toDelete = new List<Annotation>();

            foreach (Annotation annotation in page.Annotations)
            {
                // Log deletion details before removing the annotation
                using (var logWriter = new StreamWriter(logFile, true))
                {
                    // Some annotation types expose a Name property; fallback to empty string if not available
                    string name = string.Empty;
                    try { name = annotation.Name ?? string.Empty; } catch { /* ignore if property not present */ }

                    // Use the concrete annotation type name for readability (e.g., TextAnnotation, LinkAnnotation)
                    string type = annotation.GetType().Name;

                    logWriter.WriteLine($"{pageNum},{name},{type}");
                }

                toDelete.Add(annotation);
            }

            // Delete the collected annotations
            foreach (var ann in toDelete)
            {
                page.Annotations.Delete(ann);
            }
        }

        // Save the cleaned PDF
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"Annotations removed. Log written to '{logFile}'. Output PDF saved as '{outputPdf}'.");
    }
}
