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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the rectangle where the watermark will appear
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a WatermarkAnnotation for the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Generate a dynamic date string (you can customize the format)
                string dateString = DateTime.Now.ToString("yyyy-MM-dd");

                // Optionally include the page number in the watermark text
                string[] textLines = { $"{dateString} – Page {i}" };

                // Define the visual style of the watermark text
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Gray,
                    // You can adjust opacity via the annotation's Opacity property if needed
                };

                // Apply the text and its style to the watermark annotation
                watermark.SetTextAndState(textLines, textState);

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF; the Document.Save method writes a PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}