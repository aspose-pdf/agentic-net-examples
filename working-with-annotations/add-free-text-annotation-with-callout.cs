using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;          // for DefaultAppearance
using System.Drawing;          // required for DefaultAppearance color

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to annotate (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that will contain the free‑text annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // DefaultAppearance requires System.Drawing.Color for the text color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with the appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "This is a free‑text annotation with a callout.",
                Color    = Aspose.Pdf.Color.Yellow   // background color of the annotation box
            };

            // Callout requires exactly three points: start (inside), knee (bend), end (target)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 550), // start point inside the annotation
                new Aspose.Pdf.Point(200, 620), // knee point (bend)
                new Aspose.Pdf.Point(250, 650)  // end point (pointing to target location)
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with callout saved to '{outputPath}'.");
    }
}