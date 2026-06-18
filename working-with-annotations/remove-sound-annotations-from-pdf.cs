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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page in the document
            foreach (Page page in doc.Pages)
            {
                // Collect all SoundAnnotation instances on the current page
                List<Annotation> soundAnnotations = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is SoundAnnotation)
                        soundAnnotations.Add(ann);
                }

                // Delete the collected SoundAnnotations from the page's annotation collection
                foreach (Annotation ann in soundAnnotations)
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