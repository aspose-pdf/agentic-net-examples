using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least six pages (1‑based indexing)
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document contains fewer than 6 pages.");
                return;
            }

            // Get page six
            Page page = doc.Pages[6];

            // Desired watermark size (you can adjust as needed)
            double wmWidth = 200;   // width in points
            double wmHeight = 100;  // height in points

            // Calculate rectangle so the watermark is centered on the page
            double left   = (page.PageInfo.Width  - wmWidth)  / 2;
            double bottom = (page.PageInfo.Height - wmHeight) / 2;

            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                left,               // lower‑left X
                bottom,             // lower‑left Y
                left + wmWidth,     // upper‑right X
                bottom + wmHeight); // upper‑right Y

            // Create the WatermarkAnnotation with 30 % opacity
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                Opacity = 0.3 // 30 % opacity
            };

            // Add the annotation to the page
            page.Annotations.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotation added to page 6 and saved as '{outputPath}'.");
    }
}