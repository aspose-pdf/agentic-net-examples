using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annotations = page.Annotations;

                // Gather all ScreenAnnotation instances on this page
                List<Annotation> toRemove = new List<Annotation>();
                foreach (Annotation ann in annotations)
                {
                    if (ann is ScreenAnnotation)
                        toRemove.Add(ann);
                }

                // Remove each collected ScreenAnnotation
                foreach (Annotation ann in toRemove)
                {
                    annotations.Delete(ann);
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All ScreenAnnotations removed. Saved to '{outputPath}'.");
    }
}