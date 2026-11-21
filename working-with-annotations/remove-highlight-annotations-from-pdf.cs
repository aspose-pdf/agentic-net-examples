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
        const string outputPath = "output_no_highlights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Collect highlight annotations to remove
                List<Annotation> highlights = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is HighlightAnnotation)
                        highlights.Add(annot);
                }

                // Remove the collected highlight annotations
                foreach (Annotation hl in highlights)
                {
                    page.Annotations.Delete(hl);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations removed. Saved to '{outputPath}'.");
    }
}