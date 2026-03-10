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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the watermark annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 550);

            // Create the WatermarkAnnotation instance
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

            // Markdown‑style text lines for the watermark
            string[] lines = new string[]
            {
                "**Confidential**",
                "_Do not distribute_"
            };

            // Configure text appearance using TextState
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 24,
                ForegroundColor = Aspose.Pdf.Color.Red,
                // Combine Bold and Italic flags because BoldItalic is not defined in this version
                FontStyle = FontStyles.Bold | FontStyles.Italic
            };

            // Apply the text and its visual state to the annotation
            watermark.SetTextAndState(lines, textState);

            // Optional visual tweaks
            watermark.Opacity = 0.5;                     // Semi‑transparent
            watermark.Color = Aspose.Pdf.Color.LightGray; // Background color

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotation added and saved to '{outputPath}'.");
    }
}