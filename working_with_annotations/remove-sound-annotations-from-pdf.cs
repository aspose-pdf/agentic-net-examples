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

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing as per rule)
            foreach (Page page in doc.Pages)
            {
                // Collect all SoundAnnotation instances on the current page
                List<Annotation> soundAnnotations = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is SoundAnnotation)
                    {
                        soundAnnotations.Add(ann);
                    }
                }

                // Delete the collected SoundAnnotations from the page's annotation collection
                foreach (Annotation soundAnn in soundAnnotations)
                {
                    page.Annotations.Delete(soundAnn);
                }
            }

            // Save the modified PDF (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All SoundAnnotations removed. Saved to '{outputPath}'.");
    }
}