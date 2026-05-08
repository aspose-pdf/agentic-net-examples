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
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the rectangle where the watermark will appear
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 300, 800);

                // Create the WatermarkAnnotation for the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Build a dynamic date string (you can customize the format)
                string dateString = DateTime.Now.ToString("yyyy-MM-dd");
                string watermarkText = $"Generated on {dateString} – Page {i}";

                // Define the visual style of the text
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Gray,
                    // Optional: make the text semi‑transparent
                    // Opacity = 0.5f   // not a TextState property; use watermark.Opacity if needed
                };

                // Set the text and its appearance state
                watermark.SetTextAndState(new[] { watermarkText }, textState);

                // Optional: adjust opacity of the annotation itself
                watermark.Opacity = 0.5f;

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}