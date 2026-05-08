using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the base rectangle for the watermark (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle baseRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the original WatermarkAnnotation on the first page
            WatermarkAnnotation original = new WatermarkAnnotation(doc.Pages[1], baseRect)
            {
                Opacity  = 0.3,                                 // semi‑transparent
                Color    = Aspose.Pdf.Color.LightGray,          // gray tint
                Contents = "CONFIDENTIAL"                       // visible text
            };
            doc.Pages[1].Annotations.Add(original);

            // Apply a cloned watermark to each subsequent page, shifting it vertically
            for (int pageIndex = 2; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Attempt to clone the original annotation
                WatermarkAnnotation cloned = original.Clone() as WatermarkAnnotation;

                // If Clone() returns null (as documented), create a new instance and copy properties
                if (cloned == null)
                {
                    // Adjust the rectangle position for the current page
                    double yOffset = 20 * (pageIndex - 1); // move down 20 units per page
                    Aspose.Pdf.Rectangle shiftedRect = new Aspose.Pdf.Rectangle(
                        baseRect.LLX,
                        baseRect.LLY - yOffset,
                        baseRect.URX,
                        baseRect.URY - yOffset);

                    cloned = new WatermarkAnnotation(doc.Pages[pageIndex], shiftedRect)
                    {
                        Opacity  = original.Opacity,
                        Color    = original.Color,
                        Contents = original.Contents
                    };
                }
                else
                {
                    // When Clone() succeeds, update the page reference and rectangle
                    // (Page property is read‑only, so we recreate the rectangle)
                    double yOffset = 20 * (pageIndex - 1);
                    Aspose.Pdf.Rectangle shiftedRect = new Aspose.Pdf.Rectangle(
                        baseRect.LLX,
                        baseRect.LLY - yOffset,
                        baseRect.URX,
                        baseRect.URY - yOffset);
                    cloned = new WatermarkAnnotation(doc.Pages[pageIndex], shiftedRect)
                    {
                        Opacity  = cloned.Opacity,
                        Color    = cloned.Color,
                        Contents = cloned.Contents
                    };
                }

                // Add the (cloned or recreated) watermark to the current page
                doc.Pages[pageIndex].Annotations.Add(cloned);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarks applied and saved to '{outputPath}'.");
    }
}