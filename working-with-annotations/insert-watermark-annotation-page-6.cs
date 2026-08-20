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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 6 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document contains fewer than 6 pages.");
                return;
            }

            Page page = doc.Pages[6];

            // Define the size of the watermark annotation (e.g., 200 × 100 points)
            double wmWidth = 200;
            double wmHeight = 100;

            // Calculate rectangle coordinates to centre the watermark on the page
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            double llx = (pageWidth - wmWidth) / 2;   // lower‑left X
            double lly = (pageHeight - wmHeight) / 2; // lower‑left Y
            double urx = llx + wmWidth;               // upper‑right X
            double ury = lly + wmHeight;              // upper‑right Y

            // Fully qualified rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create the WatermarkAnnotation, set 30 % opacity, and add it to the page
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                Opacity = 0.3 // 30 percent opacity
            };
            page.Annotations.Add(watermark);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotation added to page 6 and saved as '{outputPath}'.");
    }
}