using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for FormattedText
using Aspose.Pdf.Text;   // for TextState if needed

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

        // Load the PDF document and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the rectangle where the watermark annotation will appear
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 550, 800);

                // Create a WatermarkAnnotation on the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // FormattedText constructor requires System.Drawing.Color for the text color
                // Use "#" as the placeholder; Aspose.Pdf will replace it with the page number
                FormattedText ft = new FormattedText(
                    "#",                         // placeholder for page number
                    System.Drawing.Color.Black, // text color
                    "Helvetica",                 // font name
                    EncodingType.Winansi,        // encoding
                    false,                       // embedded flag
                    12);                         // font size

                // Assign the formatted text to the annotation
                watermark.SetText(ft);

                // Optional: set appearance properties
                watermark.Color   = Aspose.Pdf.Color.LightGray; // annotation border color
                watermark.Opacity = 0.5;                         // semi‑transparent

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}