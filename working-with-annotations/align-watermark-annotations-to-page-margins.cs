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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define uniform margins (in points)
        const double leftMargin   = 50;
        const double bottomMargin = 50;
        const double rightMargin  = 50;
        const double topMargin    = 50;

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over a copy of the annotations collection to avoid modification issues
                foreach (Annotation ann in page.Annotations)
                {
                    // Process only WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation watermark)
                    {
                        // Compute new rectangle aligned to the page margins
                        double llx = leftMargin;
                        double lly = bottomMargin;
                        double urx = page.PageInfo.Width - rightMargin;
                        double ury = page.PageInfo.Height - topMargin;

                        // Set the new rectangle
                        watermark.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted WatermarkAnnotations saved to '{outputPath}'.");
    }
}