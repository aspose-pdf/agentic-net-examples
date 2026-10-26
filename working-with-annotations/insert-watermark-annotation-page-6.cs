using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least six pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document does not contain page 6.");
                return;
            }

            // Get page six (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[6];

            // Retrieve page dimensions
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Define watermark size (50% of page size) and compute centered rectangle
            double wmWidth = pageWidth * 0.5;
            double wmHeight = pageHeight * 0.5;
            double llx = (pageWidth - wmWidth) / 2; // lower‑left X
            double lly = (pageHeight - wmHeight) / 2; // lower‑left Y
            double urx = llx + wmWidth;               // upper‑right X
            double ury = lly + wmHeight;              // upper‑right Y

            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create the watermark annotation
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // The text of the watermark
                Contents = "CONFIDENTIAL",
                // Set opacity to 30% (0.3)
                Opacity = 0.3,
                // Set the text colour (red in this example)
                Color = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0)
            };

            // Add the annotation to the page
            page.Annotations.Add(watermark);

            // Save the modified document (inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark added and saved to '{outputPath}'.");
    }
}
