using System;
using System.Collections.Generic;
using System.IO;
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

        // Load the PDF document using the high‑level Document API
        Document doc = new Document(inputPath);

        // Pages are 1‑based in Aspose.Pdf
        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
        {
            Page page = doc.Pages[pageNum];
            // Collect the annotations that are of type LineAnnotation
            List<Annotation> toRemove = new List<Annotation>();
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is LineAnnotation)
                    toRemove.Add(annot);
            }

            // Remove the collected line annotations
            foreach (Annotation annot in toRemove)
            {
                page.Annotations.Delete(annot);
            }
        }

        // Save the updated PDF
        doc.Save(outputPath);
        Console.WriteLine($"All line annotations removed. Saved to '{outputPath}'.");
    }
}
