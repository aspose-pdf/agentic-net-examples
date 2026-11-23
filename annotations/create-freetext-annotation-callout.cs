using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color used in DefaultAppearance
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

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the FreeText annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the DefaultAppearance required by the FreeTextAnnotation constructor
            // DefaultAppearance expects a System.Drawing.Color, so we fully qualify it
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeText annotation with the required appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "This is a free text annotation with a callout.",
                // Background colour of the annotation (optional) – Aspose.Pdf.Color is required here
                Color = Aspose.Pdf.Color.LightYellow,
                // Set the intent to a callout so the Callout property is respected
                Intent = FreeTextIntent.FreeTextCallout
            };

            // Define three callout points (target, knee, and box edge)
            freeText.Callout = new[]
            {
                new Aspose.Pdf.Point(120, 520), // target point (where the arrow points to)
                new Aspose.Pdf.Point(150, 540), // knee point (break in the leader line)
                new Aspose.Pdf.Point(180, 560)  // point on the annotation rectangle edge
            };

            // Optionally set the line ending style for the callout leader
            freeText.EndingStyle = LineEnding.OpenArrow;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeText annotation with callout saved to '{outputPath}'.");
    }
}
