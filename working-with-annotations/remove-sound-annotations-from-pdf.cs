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
        const string outputPath = "output_no_sounds.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Collect all SoundAnnotation instances on the current page
                List<Annotation> soundAnnotations = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is SoundAnnotation)
                        soundAnnotations.Add(annot);
                }

                // Remove the collected SoundAnnotations from the page
                foreach (Annotation soundAnnot in soundAnnotations)
                {
                    page.Annotations.Delete(soundAnnot);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All SoundAnnotations removed. Saved to '{outputPath}'.");
    }
}