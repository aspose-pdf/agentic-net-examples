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
        const string outputPath = "filtered.pdf";
        const string currentUser = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Collect annotations whose author (Title) does NOT match the current user
                List<Annotation> toRemove = new List<Annotation>();
                foreach (Annotation annot in annotations)
                {
                    // Title is defined only on markup annotations (e.g., TextAnnotation, HighlightAnnotation, etc.)
                    if (annot is MarkupAnnotation markup)
                    {
                        if (!string.Equals(markup.Title, currentUser, StringComparison.OrdinalIgnoreCase))
                        {
                            toRemove.Add(annot);
                        }
                    }
                    else
                    {
                        // Non‑markup annotations do not have an author; keep them unchanged.
                    }
                }

                // Remove the collected annotations
                foreach (Annotation annot in toRemove)
                {
                    annotations.Delete(annot);
                }
            }

            // Save the filtered PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
    }
}
