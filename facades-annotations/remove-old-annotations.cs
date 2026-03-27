using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";
        const int daysThreshold = 30; // remove annotations older than 30 days

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        DateTime cutoffDate = DateTime.Now.AddDays(-daysThreshold);

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    AnnotationCollection annotations = page.Annotations;
                    List<int> indicesToDelete = new List<int>();

                    // Collect indices of annotations that are older than the cutoff date
                    for (int annIndex = 1; annIndex <= annotations.Count; annIndex++)
                    {
                        Annotation annotation = annotations[annIndex];
                        if (annotation is MarkupAnnotation markupAnnotation)
                        {
                            // The Modified property holds the last modification date of the annotation
                            if (markupAnnotation.Modified < cutoffDate)
                            {
                                indicesToDelete.Add(annIndex);
                            }
                        }
                    }

                    // Delete collected annotations starting from the highest index to avoid re‑indexing issues
                    for (int i = indicesToDelete.Count - 1; i >= 0; i--)
                    {
                        annotations.Delete(indicesToDelete[i]);
                    }
                }

                doc.Save(outputPath);
                Console.WriteLine($"Old annotations removed. Saved cleaned PDF to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}