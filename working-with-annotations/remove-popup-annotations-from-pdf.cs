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

        // Load the PDF (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annColl = page.Annotations;

                // Gather popup annotations first to avoid modifying the collection while iterating
                List<Annotation> popups = new List<Annotation>();
                foreach (Annotation ann in annColl)
                {
                    if (ann is PopupAnnotation)
                    {
                        popups.Add(ann);
                    }
                }

                // Delete each popup annotation; parent markup annotations remain intact
                foreach (Annotation popup in popups)
                {
                    annColl.Delete(popup);
                }
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All popup annotations removed. Saved to '{outputPath}'.");
    }
}