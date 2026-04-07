using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through all annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Process only HighlightAnnotation instances
                    if (ann is HighlightAnnotation highlight)
                    {
                        // Change the annotation color to light green
                        highlight.Color = Aspose.Pdf.Color.LightGreen;
                    }
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations recolored and saved to '{outputPath}'.");
    }
}