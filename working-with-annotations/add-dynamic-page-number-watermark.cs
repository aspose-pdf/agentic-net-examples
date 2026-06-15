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
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the rectangle where the watermark will appear
                // (left, bottom, right, top) – adjust as needed
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the WatermarkAnnotation for the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Build the FormattedText that contains the page number and its style.
                // Fully‑qualified type names are used to avoid importing the Facades namespace.
                var formattedText = new Aspose.Pdf.Facades.FormattedText(
                    i.ToString(),                                 // text (page number)
                    System.Drawing.Color.Gray,                    // foreground colour
                    "Helvetica",                                 // font name (built‑in)
                    Aspose.Pdf.Facades.EncodingType.Winansi,      // encoding
                    false,                                         // embed font flag
                    24);                                           // font size

                // Assign the formatted text to the annotation.
                // SetText expects a FormattedText instance, not a plain string.
                watermark.SetText(formattedText);

                // Optional visual settings for the annotation border and opacity.
                watermark.Color = Color.LightGray; // annotation border colour
                watermark.Opacity = 0.5;            // semi‑transparent

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
