using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (page indexing is 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through annotations on the page
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // Check if the annotation is a figure annotation (Square, Circle, etc.)
                    if (ann is CommonFigureAnnotation figureAnn)
                    {
                        // Retrieve the appearance dictionary (contains Normal, Rollover, Down streams)
                        AppearanceDictionary appearance = figureAnn.Appearance;

                        // Example: access the normal appearance stream (if needed)
                        // var normalStream = appearance.Normal; // Stream object

                        // Modify the annotation's color (this updates the appearance)
                        figureAnn.Color = Aspose.Pdf.Color.Red;

                        // Note: UpdateAppearance() is not required and does not exist on CommonFigureAnnotation.
                        // The color change automatically updates the appearance.

                        Console.WriteLine($"Modified color of figure annotation on page {i}, index {j}.");
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
