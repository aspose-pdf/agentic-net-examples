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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // First page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Rectangle that defines the annotation bounds (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // FreeTextAnnotation requires a DefaultAppearance (font, size, colour)
            // DefaultAppearance expects a System.Drawing.Color, so we fully qualify it.
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the annotation – note the three‑argument constructor (page, rectangle, appearance)
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "This is a free‑text annotation with a callout.",
                Title = "Note",
                // Background colour of the annotation box (Aspose.Pdf.Color, not System.Drawing.Color)
                Color = Aspose.Pdf.Color.Yellow
            };

            // Border must be assigned after the annotation instance exists
            freeText.Border = new Border(freeText) { Width = 1 };

            // Callout line: first point is the tip inside the box, second point is the anchor on the page
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 550), // tip inside the annotation
                new Aspose.Pdf.Point(200, 400)  // target point on the page
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with callout saved to '{outputPath}'.");
    }
}
