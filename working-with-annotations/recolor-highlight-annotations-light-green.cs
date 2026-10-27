using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_green.pdf";

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

                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only highlight annotations
                    if (annot is HighlightAnnotation highlight)
                    {
                        // Set the highlight color to light green (cross‑platform Aspose.Pdf.Color)
                        highlight.Color = Aspose.Pdf.Color.LightGreen;
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations recolored to light green and saved to '{outputPath}'.");
    }
}