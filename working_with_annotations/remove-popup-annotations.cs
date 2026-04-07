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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annots = page.Annotations;

                // Collect popup annotations first to avoid modifying the collection while iterating
                List<PopupAnnotation> popups = new List<PopupAnnotation>();
                foreach (Annotation ann in annots)
                {
                    if (ann is PopupAnnotation popup)
                    {
                        popups.Add(popup);
                    }
                }

                // Delete each popup annotation; the parent markup annotation remains untouched
                foreach (PopupAnnotation popup in popups)
                {
                    annots.Delete(popup);
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup annotations removed. Saved to '{outputPath}'.");
    }
}