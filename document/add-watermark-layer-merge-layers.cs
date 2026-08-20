using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations; // for ImageStamp

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // existing PDF
        const string watermarkImagePath = "watermark.png"; // watermark image file
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Create a new optional content group (layer) that will hold the watermark
            Layer wmLayer = new Layer("WatermarkLayer", "OCG_WM");

            // Prepare the image stamp that will act as the watermark
            ImageStamp imgStamp = new ImageStamp(watermarkImagePath)
            {
                // Position and size – you can adjust these values as needed
                // Width/Height are in points (1/72 inch). Here we set a modest size.
                Width = 200,
                Height = 100,
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                // Make the watermark semi‑transparent
                Opacity = 0.3f
            };

            // Add the watermark layer and the stamp to each page, then merge layers
            foreach (Page page in doc.Pages)
            {
                // Attach the watermark layer to the page
                page.Layers.Add(wmLayer);

                // Add the image stamp (watermark) to the page content
                page.AddStamp(imgStamp);

                // Merge all layers on this page into a single layer named "MergedLayer"
                page.MergeLayers("MergedLayer");
            }

            // Save the modified PDF (lifecycle rule: use using, then Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with watermark layer saved to '{outputPdf}'.");
    }
}
