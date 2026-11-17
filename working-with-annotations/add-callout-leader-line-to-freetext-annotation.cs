using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for Aspose.Pdf.Text.Point and DefaultAppearance

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a free‑text annotation on the first page
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Build the required DefaultAppearance for the FreeTextAnnotation constructor
            // NOTE: DefaultAppearance expects System.Drawing.Color, so we fully qualify it to avoid ambiguity.
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with the required appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Set the visible text of the annotation
                Contents = "This is a free‑text annotation with a callout leader line."
            };

            // Define the callout (leader line) points – an array of Aspose.Pdf.Point objects
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(80, 520), // start point (outside the annotation box)
                new Aspose.Pdf.Point(100, 540) // end point (where the line meets the annotation box)
            };

            // Optionally set the intent to indicate this is a callout annotation
            freeText.Intent = FreeTextIntent.FreeTextCallout;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with callout saved to '{outputPath}'.");
    }
}
