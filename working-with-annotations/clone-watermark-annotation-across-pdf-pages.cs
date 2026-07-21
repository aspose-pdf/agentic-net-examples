using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the initial rectangle for the watermark (left, bottom, right, top)
            Aspose.Pdf.Rectangle baseRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the original watermark annotation on the first page
            WatermarkAnnotation baseWatermark = new WatermarkAnnotation(doc.Pages[1], baseRect)
            {
                Color = Aspose.Pdf.Color.LightGray,
                Opacity = 0.5,
                Contents = "Sample Watermark"
            };
            doc.Pages[1].Annotations.Add(baseWatermark);

            // Apply a cloned watermark to each subsequent page with a vertical offset
            for (int i = 2; i <= doc.Pages.Count; i++)
            {
                // Attempt to clone the original annotation
                WatermarkAnnotation cloned = (WatermarkAnnotation)baseWatermark.Clone();

                // If Clone returns null (as per API behavior), create a new instance copying properties
                if (cloned == null)
                {
                    cloned = new WatermarkAnnotation(doc.Pages[i], baseRect)
                    {
                        Color = baseWatermark.Color,
                        Opacity = baseWatermark.Opacity,
                        Contents = baseWatermark.Contents
                    };
                }
                else
                {
                    // Ensure the cloned annotation is attached to the current page
                    cloned = new WatermarkAnnotation(doc.Pages[i], cloned.Rect)
                    {
                        Color = baseWatermark.Color,
                        Opacity = baseWatermark.Opacity,
                        Contents = baseWatermark.Contents
                    };
                }

                // Adjust the rectangle position (move down by 20 points per page)
                double offsetY = 20 * (i - 1);
                cloned.Rect = new Aspose.Pdf.Rectangle(
                    baseRect.LLX,
                    baseRect.LLY - offsetY,
                    baseRect.URX,
                    baseRect.URY - offsetY);

                // Add the cloned watermark to the current page
                doc.Pages[i].Annotations.Add(cloned);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}