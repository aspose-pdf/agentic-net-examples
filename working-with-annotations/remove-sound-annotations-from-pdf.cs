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
        const string outputPath = "output_no_sound.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate through each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Gather all SoundAnnotation instances on the current page
                List<Annotation> soundAnnotations = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is SoundAnnotation)
                    {
                        soundAnnotations.Add(ann);
                    }
                }

                // Delete each collected SoundAnnotation
                foreach (Annotation ann in soundAnnotations)
                {
                    page.Annotations.Delete(ann);
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All SoundAnnotations removed. Saved to '{outputPath}'.");
    }
}