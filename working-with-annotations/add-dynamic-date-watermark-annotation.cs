using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Generate a dynamic date string for this page
                string dateText = DateTime.Now.ToString("yyyy-MM-dd");

                // Define the rectangle where the watermark annotation will appear
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the WatermarkAnnotation for the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Prepare a TextState for styling (optional)
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Color.Gray,
                    BackgroundColor = Color.Transparent,
                    FontStyle = FontStyles.Bold
                };

                // Set the dynamic text and its appearance state
                watermark.SetTextAndState(new[] { dateText }, textState);

                // Optionally set opacity (0.0 = fully transparent, 1.0 = fully opaque)
                watermark.Opacity = 0.5;

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(watermark);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}