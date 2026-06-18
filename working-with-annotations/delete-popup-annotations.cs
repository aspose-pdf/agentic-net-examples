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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Collect all PopupAnnotation instances on the current page
                List<Annotation> popups = new List<Annotation>();
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];
                    if (ann is PopupAnnotation)
                    {
                        popups.Add(ann);
                    }
                }

                // Delete each collected popup annotation, preserving its parent markup annotation
                foreach (Annotation popup in popups)
                {
                    page.Annotations.Delete(popup);
                }
            }

            // Save the modified PDF (lifecycle rule: save after all changes)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All popup annotations removed. Output saved to '{outputPath}'.");
    }
}