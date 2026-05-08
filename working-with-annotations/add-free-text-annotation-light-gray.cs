using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Drawing; // for System.Drawing.Color used by DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // Define the annotation rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance (requires System.Drawing.Color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "This is a free‑text annotation with a light gray background.",
                // Background color
                Color = Aspose.Pdf.Color.LightGray
                // Note: FreeTextAnnotation does not expose an 'Open' property.
            };

            // Configure a simple solid border.
            freeText.Border = new Border(freeText)
            {
                Width = 1
                // Rounded corners are not directly supported by the Border class.
                // If a visual effect resembling rounded corners is required,
                // consider using a Cloudy border effect or drawing a custom shape.
                // Example (optional):
                // BorderEffect = new BorderEffect(BorderEffectType.Cloudy) { Intensity = 2 }
            };

            page.Annotations.Add(freeText);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
