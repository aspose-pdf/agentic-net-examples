using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_watermarks.pdf";

        // Define uniform margins (in points; 1 inch = 72 points)
        const double leftMargin   = 36;   // 0.5 inch
        const double rightMargin  = 36;
        const double topMargin    = 36;
        const double bottomMargin = 36;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over a copy of the annotations collection to avoid modification issues
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only WatermarkAnnotation instances
                    if (annot is WatermarkAnnotation watermark)
                    {
                        // Get the page size (media box)
                        Aspose.Pdf.Rectangle pageRect = page.Rect;

                        // Calculate new rectangle respecting the uniform margins
                        double llx = pageRect.LLX + leftMargin;
                        double lly = pageRect.LLY + bottomMargin;
                        double urx = pageRect.URX - rightMargin;
                        double ury = pageRect.URY - topMargin;

                        // Assign the new rectangle to the watermark annotation
                        watermark.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotations aligned and saved to '{outputPath}'.");
    }
}