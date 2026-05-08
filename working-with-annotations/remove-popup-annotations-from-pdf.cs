using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_without_popups.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Collect popup annotations to delete after iteration
                List<Annotation> popups = new List<Annotation>();

                // page.Annotations is 1‑based; enumerate safely with foreach
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is PopupAnnotation)
                        popups.Add(ann);
                }

                // Delete each popup annotation while preserving its parent markup annotation
                foreach (Annotation popup in popups)
                {
                    page.Annotations.Delete(popup);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup annotations removed. Saved to '{outputPath}'.");
    }
}