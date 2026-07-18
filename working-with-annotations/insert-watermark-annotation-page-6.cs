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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 6 pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document does not contain page 6.");
                return;
            }

            // Get page 6 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[6];

            // Determine page size
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Create a rectangle that covers the whole page (centered)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, pageHeight);

            // Create the WatermarkAnnotation on page 6
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // Set opacity to 30% (0.3)
                Opacity = 0.3
            };

            // Add the annotation to the page
            page.Annotations.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotation added. Saved to '{outputPath}'.");
    }
}