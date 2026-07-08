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
        const double margin = 50; // uniform margin from each page edge

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing is handled by the foreach)
            foreach (Page page in doc.Pages)
            {
                // Iterate through annotations on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Process only WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation watermark)
                    {
                        // Calculate new rectangle aligned with the page margins
                        double left   = margin;
                        double bottom = margin;
                        double right  = page.PageInfo.Width  - margin;
                        double top    = page.PageInfo.Height - margin;

                        // Set the updated rectangle
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