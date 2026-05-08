using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_custom_highlight.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through annotations on the page
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // Process only HighlightAnnotation instances
                    if (ann is HighlightAnnotation highlight)
                    {
                        // Retrieve the appearance dictionary (read‑only property)
                        // The appearance streams are automatically regenerated when the
                        // annotation's visual properties change, so we modify the color.
                        // Custom shade: a light orange (RGB 255,200,100)
                        highlight.Color = Aspose.Pdf.Color.FromRgb(1.0, 0.78, 0.39);

                        // Force regeneration of the appearance stream (optional)
                        // Setting the color is sufficient; the library updates the stream.
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}