using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlight_color_changed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard load constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through all annotations on the page
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // Process only HighlightAnnotation instances
                    if (ann is HighlightAnnotation highlight)
                    {
                        // Retrieve the appearance dictionary (read‑only, for inspection only)
                        var appearanceDict = highlight.Appearance;
                        // (The appearance stream can be inspected via appearanceDict if needed)

                        // Replace the annotation color with a custom shade (e.g., a light orange)
                        highlight.Color = Aspose.Pdf.Color.FromRgb(1.0, 0.6, 0.2); // RGB values are 0‑1

                        // Optionally force regeneration of the appearance stream
                        // (Setting the color updates the normal appearance automatically)
                    }
                }
            }

            // Save the modified PDF (standard save, no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}