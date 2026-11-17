using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_aligned.pdf";

        // Define uniform margins (in points)
        const double marginLeft   = 50;
        const double marginRight  = 50;
        const double marginTop    = 50;
        const double marginBottom = 50;

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
                foreach (Annotation ann in page.Annotations)
                {
                    // Process only WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation watermark)
                    {
                        // Compute new rectangle aligned to the defined margins
                        double left   = marginLeft;
                        double bottom = marginBottom;
                        double right  = page.PageInfo.Width  - marginRight;
                        double top    = page.PageInfo.Height - marginTop;

                        // Set the rectangle (bounds) of the watermark annotation
                        // The correct property name for WatermarkAnnotation is 'Rect'
                        watermark.Rect = new Aspose.Pdf.Rectangle(left, bottom, right, top);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotations aligned and saved to '{outputPath}'.");
    }
}
