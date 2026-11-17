using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to place the annotation (first page)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            // Coordinates: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create the default appearance (font, size, colour) required by FreeTextAnnotation
            // DefaultAppearance expects a System.Drawing.Color, so we fully qualify it to avoid ambiguity.
            DefaultAppearance appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation using the constructor that accepts Page, Rectangle and DefaultAppearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // The visible text of the annotation
                Contents = "This is a free‑text annotation."
            };

            // Set the annotation border (no border). Border requires the parent annotation in its constructor.
            freeText.Border = new Border(freeText) { Width = 0 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
