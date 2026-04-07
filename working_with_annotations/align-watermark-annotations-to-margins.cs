using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermark_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define a uniform margin (in points) to apply to all sides
            const double margin = 50.0;

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Iterate over a copy of the annotation collection because we may modify it
                foreach (Annotation ann in page.Annotations)
                {
                    // Process only WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation watermark)
                    {
                        // Create a new rectangle aligned with the page margins
                        // left, bottom, right, top
                        Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                            margin,                     // left
                            margin,                     // bottom
                            pageWidth  - margin,        // right
                            pageHeight - margin         // top
                        );

                        // Assign the new rectangle to the annotation
                        watermark.Rect = newRect;
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotations aligned and saved to '{outputPath}'.");
    }
}