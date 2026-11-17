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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Free‑text annotation requires a DefaultAppearance (font, size, text colour)
            var appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);
            FreeTextAnnotation freeText = new FreeTextAnnotation(doc.Pages[1], rect, appearance)
            {
                Title = "Note",
                Contents = "This is a free‑text annotation with light gray background and rounded corners.",
                // Background colour is the inherited Color property
                Color = Aspose.Pdf.Color.LightGray
            };

            // Set a solid border of width 1 and rounded corners (5 points radius)
            freeText.Border = new Border(freeText)
            {
                Width = 1,
                HCornerRadius = 5,
                VCornerRadius = 5,
                Style = BorderStyle.Solid
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
