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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance required by FreeTextAnnotation constructor
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create a free‑text annotation on the specified page with the appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance);
            freeText.Contents = "Free‑text annotation with a callout leader line.";

            // Configure the annotation as a callout and set the leader line length to 20 points
            // The leader line length is defined by the distance between the first two points of the Callout array.
            freeText.Intent = FreeTextIntent.FreeTextCallout;
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                // First point (anchor) – placed at the centre of the annotation rectangle for simplicity
                new Aspose.Pdf.Point(rect.LLX + (rect.URX - rect.LLX) / 2, rect.LLY + (rect.URY - rect.LLY) / 2),
                // Second point – 20 points to the right of the first point (leader line length)
                new Aspose.Pdf.Point(rect.LLX + (rect.URX - rect.LLX) / 2 + 20, rect.LLY + (rect.URY - rect.LLY) / 2)
            };
            // Optional: show an arrow at the end of the leader line
            freeText.EndingStyle = LineEnding.OpenArrow;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
