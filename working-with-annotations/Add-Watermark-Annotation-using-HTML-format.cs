using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the rectangle where the watermark will appear
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 550);

                // Create a WatermarkAnnotation on the page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
                {
                    // Set visual properties
                    Color = Aspose.Pdf.Color.LightGray,
                    Opacity = 0.5,
                    Contents = "CONFIDENTIAL"
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(watermark);

                // Prepare HTML save options (must be passed explicitly)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the modified document as HTML
                doc.Save(outputPath, htmlOpts);
            }

            Console.WriteLine($"Watermark added and saved as HTML to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}