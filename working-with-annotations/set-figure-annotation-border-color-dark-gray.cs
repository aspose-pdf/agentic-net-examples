using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define dark gray color using Aspose.Pdf.Color (cross‑platform)
            Aspose.Pdf.Color darkGray = Aspose.Pdf.Color.FromRgb(0.33, 0.33, 0.33);

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over each annotation on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Figure annotations (Square, Circle, Polygon, etc.) inherit from MarkupAnnotation.
                    // The border colour is set via the Color property, not BorderColor.
                    if (annot is MarkupAnnotation markup)
                    {
                        markup.Color = darkGray;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All figure annotation borders set to dark gray. Saved to '{outputPath}'.");
    }
}
