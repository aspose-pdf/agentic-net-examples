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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least six pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document does not contain page 6.");
                return;
            }

            // Get page six (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[6];

            // Determine page size
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Define desired watermark size (example: 200 x 100 points)
            const double watermarkWidth  = 200;
            const double watermarkHeight = 100;

            // Calculate rectangle that centers the watermark on the page
            double llx = (pageWidth  - watermarkWidth)  / 2; // lower‑left X
            double lly = (pageHeight - watermarkHeight) / 2; // lower‑left Y
            double urx = llx + watermarkWidth;               // upper‑right X
            double ury = lly + watermarkHeight;              // upper‑right Y

            // Create the WatermarkAnnotation at the calculated rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // Set opacity to 30 % (0.3)
                Opacity = 0.3
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark added and saved to '{outputPath}'.");
    }
}