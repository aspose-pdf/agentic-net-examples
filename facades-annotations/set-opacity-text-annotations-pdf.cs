using System;
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

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
        {
            Page page = doc.Pages[pageNum];
            // Annotations collection is zero‑based.
            for (int annIdx = 0; annIdx < page.Annotations.Count; annIdx++)
            {
                Annotation ann = page.Annotations[annIdx];
                if (ann is TextAnnotation txtAnn)
                {
                    // Set the required opacity.
                    txtAnn.Opacity = 0.4f;
                }
            }
        }

        // Save the modified PDF.
        doc.Save(outputPath);
        Console.WriteLine($"All Text annotations opacity set to 0.4 and saved to '{outputPath}'.");
    }
}