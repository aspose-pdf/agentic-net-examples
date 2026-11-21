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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Gather all ScreenAnnotation objects on the page
                List<ScreenAnnotation> toRemove = new List<ScreenAnnotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is ScreenAnnotation screenAnnot)
                        toRemove.Add(screenAnnot);
                }

                // Remove the collected ScreenAnnotations
                foreach (ScreenAnnotation screenAnnot in toRemove)
                {
                    page.Annotations.Delete(screenAnnot);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Screen annotations removed. Saved to '{outputPath}'.");
    }
}