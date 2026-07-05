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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the position of the watermark annotation (bottom‑center)
                // Rectangle(left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    page.PageInfo.Width / 2 - 50,   // left
                    20,                             // bottom
                    page.PageInfo.Width / 2 + 50,   // right
                    40);                            // top

                // Create the WatermarkAnnotation on the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Set the dynamic page number placeholder.
                // The placeholder character '#' will be replaced with the actual page number.
                // Use SetTextAndState to supply the placeholder string and a default TextState.
                watermark.SetTextAndState(new string[] { "Page #" }, new TextState());

                // Optional: customize appearance (color, opacity, etc.)
                watermark.Color   = Aspose.Pdf.Color.Gray;
                watermark.Opacity = 0.5;

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}