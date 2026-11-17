using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Loop through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Define the rectangle where the watermark will appear
                Rectangle rect = new Rectangle(100, 500, 400, 550);

                // Create a WatermarkAnnotation. The "#" placeholder is replaced by the current page number.
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
                {
                    Opacity = 0.5
                };

                // Define the visual style of the watermark text
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Arial"),
                    FontSize = 24,
                    ForegroundColor = Color.Gray
                };

                // Set the watermark text using the placeholder for page number
                watermark.SetTextAndState(new[] { "Page #" }, textState);

                // Add the annotation to the current page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
