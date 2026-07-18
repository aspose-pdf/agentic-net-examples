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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Collect all SoundAnnotation instances on the current page
                List<Annotation> toDelete = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is SoundAnnotation)
                        toDelete.Add(ann);
                }

                // Delete the collected SoundAnnotations
                foreach (Annotation ann in toDelete)
                {
                    page.Annotations.Delete(ann);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All SoundAnnotations removed. Saved to '{outputPath}'.");
    }
}