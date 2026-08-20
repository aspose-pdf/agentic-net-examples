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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Annotations collection is also 1‑based
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // Process only highlight annotations
                    if (ann is HighlightAnnotation highlight)
                    {
                        // Set opacity to 60% (0.6)
                        highlight.Opacity = 0.6;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations updated and saved to '{outputPath}'.");
    }
}