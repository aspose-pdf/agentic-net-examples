using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_without_sound.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Collect annotations to delete to avoid modifying the collection during enumeration
                var toDelete = new System.Collections.Generic.List<Annotation>();

                foreach (Annotation ann in annotations)
                {
                    // Identify SoundAnnotation instances
                    if (ann is SoundAnnotation)
                    {
                        toDelete.Add(ann);
                    }
                }

                // Delete the identified SoundAnnotations
                foreach (Annotation ann in toDelete)
                {
                    annotations.Delete(ann);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All SoundAnnotations removed. Saved to '{outputPath}'.");
    }
}