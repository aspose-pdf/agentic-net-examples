using System;
using System.IO;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Collect all SoundAnnotation objects on this page
                var soundAnnotations = new System.Collections.Generic.List<SoundAnnotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is SoundAnnotation soundAnn)
                    {
                        soundAnnotations.Add(soundAnn);
                    }
                }

                // Delete each collected SoundAnnotation from the page's annotation collection
                foreach (SoundAnnotation soundAnn in soundAnnotations)
                {
                    page.Annotations.Delete(soundAnn);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sound annotations removed. Saved to '{outputPath}'.");
    }
}